using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TetrisPageRank.Models;

namespace TetrisPageRank
{
    public class Ranker
    {
        public Iteration CurrentIteration { get; private set; }

        public Ranker(string iterationFileName = null)
        {
            if (string.IsNullOrEmpty(iterationFileName) || !File.Exists(iterationFileName))
            {
                CurrentIteration = new Iteration();
                CurrentIteration.Initialize();
            }
            else
            {
                using var openFileStream = new StreamReader(File.OpenRead(iterationFileName));
                using var reader = new JsonTextReader(openFileStream);
                CurrentIteration = new Iteration();

                reader.Read(); // {
                reader.Read(); // "iterationCount"

                CurrentIteration.iterationCount = reader.ReadAsInt32().Value;

                reader.Read(); // "stackRanks"
                reader.Read(); // {

                CurrentIteration.stackRanks = new Dictionary<int, float>(Iteration.stackCount);

                for (int i = 0; i < Iteration.stackCount; i++)
                {
                    reader.Read();
                    var stack = int.Parse(reader.Value.ToString());

                    var rank = (float)reader.ReadAsDouble();

                    CurrentIteration.stackRanks.Add(stack, rank);
                }
            }
        }

        public void Iterate(int n)
        {
            if (n <= 0)
            {
                return;
            }

            Console.WriteLine($"Iterating {n} times.");
            
            Iteration newIteration = CurrentIteration.Clone();

            for (int i = 0; i < n; i++)
            {
                newIteration.iterationCount++;
                
                Console.WriteLine($"Iteration {newIteration.iterationCount}");

                Parallel.ForEach(CurrentIteration.stackRanks, stack =>
                {
                    newIteration.stackRanks[stack.Key] = Rank(stack.Key);
                });

                var auxIteration = newIteration;
                newIteration = CurrentIteration;
                CurrentIteration = auxIteration;

                newIteration.iterationCount = CurrentIteration.iterationCount;
            }
        }

        private float Rank(int stack)
        {
            var totalRank = 0f;

            foreach (var piece in Piece.All)
            {
                totalRank += RankPiece(stack, piece);
            }

            return totalRank / Piece.All.Length;
        }

        private float RankPiece(int stack, Piece piece)
        {
            var bestRank = 0f;

            foreach (var generatedStack in piece.Controller.GetPossibleStacks(stack))
            {
                if (stack == generatedStack)
                {
                    continue;
                }

                var rank = CurrentIteration.stackRanks[generatedStack];
                if (rank > bestRank)
                {
                    bestRank = rank;
                }
            }

            return bestRank;
        }
    }
}
