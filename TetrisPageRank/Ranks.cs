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
        public static List<float> Current;
        public static List<float> Next;
        public static Dictionary<int, int> Indexes;

        public static void Initialize()
        {
            Indexes = CreateIndexDictionary();
            Current = CreateRandomRankList();
            Next = CreateNewList();
        }

        public static void InitializeFromFile(string rankFileName)
        {
            Indexes = CreateIndexDictionary();
            Current = LoadResults(rankFileName);
            Next = CreateNewList();
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
                                            dic.Add(TetrisStack.CreateStack(a, b, c, d, e, f, g, h), i++);
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

        private static List<float> CreateRandomRankList()
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

        private static List<float> CreateNewList()
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
