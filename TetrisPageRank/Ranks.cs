using BinarySerialization;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.IO;

namespace TetrisPageRank
{
    public static class Ranks
    {
        public const int STACK_COUNT = 43046721; // 9^8 
        private static List<float> Current { set; get; }
        private static List<float> Next { set; get; }
        public static Dictionary<int, int> Indexes { private set; get; }

        public static void Initialize()
        {
            Indexes = CreateIndexDictionary();
            Current = CreateRandomizedRankList();
            Next = CreateZeroedRankList();
        }

        public static void InitializeFromFile(string rankFileName)
        {
            Indexes = CreateIndexDictionary();
            Current = LoadResults(rankFileName);
            Next = CreateZeroedRankList();
        }

        public static void SaveResults(string rankFileName)
        {
            if (Current == null)
            {
                throw new Exception("The ranks were not initialized.");
            }

            using var openFileStream = File.Create(rankFileName);
            var serializer = new BinarySerializer();
            serializer.Serialize(openFileStream, Current);
        }

        public static void UpdateCurrentRankList()
        {
            // Swap ranks list to avoid allocating more memory
            List<float> aux = Current;
            Current = Next;
            Next = aux;
        }

        public static float GetCurrentRank(int stack)
        {
            return Current[Indexes[stack]];
        }

        public static void SetNextRank(int stack, float rank)
        {
            Next[Indexes[stack]] = rank;
        }

        private static Dictionary<int, int> CreateIndexDictionary()
        {
            var dic = new Dictionary<int, int>(STACK_COUNT);
            int i = 0;

            for (int a = -4; a <= 4; a++)
            {
                for (int b = -4; b <= 4; b++)
                {
                    for (int c = -4; c <= 4; c++)
                    {
                        for (int d = -4; d <= 4; d++)
                        {
                            for (int e = -4; e <= 4; e++)
                            {
                                for (int f = -4; f <= 4; f++)
                                {
                                    for (int g = -4; g <= 4; g++)
                                    {
                                        for (int h = -4; h <= 4; h++)
                                        {
                                            dic.Add(TetrisStackHelper.CreateStack(a, b, c, d, e, f, g, h), i++);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return dic;
        }

        private static List<float> CreateRandomizedRankList()
        {
            var randomizer = RandomizerFactory.GetRandomizer(new FieldOptionsFloat
            {
                Min = 0,
                Max = 1,
                UseNullValues = false
            });

            var rankList = new List<float>();

            for (int i = 0; i < STACK_COUNT; i++)
            {
                float rank = randomizer.Generate().Value;
                rankList.Add(rank);
            }

            return rankList;
        }

        private static List<float> CreateZeroedRankList()
        {
            var rankList = new List<float>();

            for (int i = 0; i < STACK_COUNT; i++)
            {
                rankList.Add(0);
            }

            return rankList;
        }

        private static List<float> LoadResults(string rankFileName)
        {
            var openFileStream = File.OpenRead(rankFileName);
            var serializer = new BinarySerializer();
            return serializer.Deserialize<List<float>>(openFileStream);
        }
    }
}
