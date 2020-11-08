using Newtonsoft.Json;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.IO;

namespace TetrisPageRank.Models
{
    [Serializable]
    public class Iteration
    {
        public int iterationCount;
        public Dictionary<int, float> stackRanks;
        public const int stackCount = 43046721;

        public void Initialize()
        {
            var stackFactory = new StackFactory();
            stackRanks = new Dictionary<int, float>(stackCount);
            var randomizer = RandomizerFactory.GetRandomizer(new FieldOptionsFloat() 
            { 
                Min = 0, 
                Max = 1, 
                UseNullValues = false 
            });

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
                                            stackRanks.Add(stackFactory.Create(a, b, c, d, e, f, g, h), randomizer.Generate().Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            iterationCount = 0;
        }

        public Iteration Clone()
        {
            return new Iteration
            {
                iterationCount = this.iterationCount,
                stackRanks = new Dictionary<int, float>(this.stackRanks)
            };
        }

        public void Save(string iterationFileName)
        {
            using var openFileStream = new StreamWriter(File.Create(iterationFileName));
            using var writer = new JsonTextWriter(openFileStream);
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, this);
        }
    }
}
