using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TetrisPageRank;

namespace KeyBloxManager
{
    public class Program
    {
        private static void Main()
        {
            int gameCount = 1;
            int pieceCount = 0;
            int ranksIterationCount = 15;
            int lookaheadCount = 4;

            Log.Logger = new LoggerConfiguration().WriteTo.File($"ranks{ranksIterationCount}_preview{lookaheadCount}.log").CreateLogger();

            ILogger consoleLogger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            var keyblox = new KeyBlox(lookaheadCount);
            keyblox.Focus();

            Log.Information("Initializing");
            Ranks.InitializeFromFile("ranks{ranksIterationCount}.dat", true);
            Log.Information("Done, ready to play");

            int[] columns = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            while (!keyblox.IsPlayable())
            {
            }

            Log.Information("Let's play");

            while (true)
            {
                //Thread.Sleep(750);

                Piece piece = Piece.AllDictionary[keyblox.GetCurrentPiece().GetName()];
                List<Piece> lookahead = keyblox.GetPreviewedPieces().Select(x => Piece.AllDictionary[x.GetName()]).ToList();
                TetrisDrop bestDrop = null;
                
                bestDrop = GetBestPossibleDrop(columns, piece, lookahead);

                if (bestDrop != null)
                {
                    //Log.Information("Piece: {0}, Column: {1}", bestDrop.Piece.Name, bestDrop.Column);
                    //Log.Information("Columns: {0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                    //    bestDrop.Columns[0],
                    //    bestDrop.Columns[1],
                    //    bestDrop.Columns[2],
                    //    bestDrop.Columns[3],
                    //    bestDrop.Columns[4],
                    //    bestDrop.Columns[5],
                    //    bestDrop.Columns[6],
                    //    bestDrop.Columns[7],
                    //    bestDrop.Columns[8]);
                    keyblox.ExecuteDrop(bestDrop);
                    columns = bestDrop.Columns;

                    while (keyblox.GetPieceCount() == pieceCount)
                    {
                    }
                    pieceCount++;

                    for (int i = 0; i < 8; i++)
                    {
                        var actualColumn = keyblox.GetColumn(i);
                        VerifyColumn(actualColumn);
                    }
                }
                else
                {
                    Log.Information("-----GAME RESULTS {0}-----", gameCount++);
                    Log.Information("Lines cleared: {0}", keyblox.GetLineCount());
                    Log.Information("Pieces used: {0}", keyblox.GetPieceCount());

                    consoleLogger.Information("-----GAME RESULTS {0}-----", gameCount);
                    consoleLogger.Information("Lines cleared: {0}", keyblox.GetLineCount());
                    consoleLogger.Information("Pieces used: {0}", keyblox.GetPieceCount());

                    columns = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    keyblox.Reset();
                    pieceCount = 0;
                    Thread.Sleep(500);
                    while (!keyblox.IsPlayable())
                    {
                    }
                }
            }
        }

        private static void VerifyColumn(int[] actualColumn)
        {
            bool shouldntHaveMorePieces = false;
            for (int i = 0; i < actualColumn.Length; i++)
            {
                if (!shouldntHaveMorePieces && actualColumn[i] == 0)
                {
                    shouldntHaveMorePieces = true;
                }

                if (shouldntHaveMorePieces && actualColumn[i] > 0)
                {
                    while (true)
                    {
                    }
                }
            }
        }

        private static TetrisDrop GetBestPossibleDrop(int[] columns, Piece piece, List<Piece> lookahead)
        {
            List<TetrisDrop> drops = piece.Service.GetPossibleDrops(columns);

            TetrisDrop bestDrop = null;
            float bestRank = -1;

            if (lookahead.Count > 0)
            {
                foreach (TetrisDrop drop in drops)
                {
                    var futureDrops = GetPossibleDrops(drop.Columns, lookahead);
                    if (futureDrops.Count == 0)
                    {
                        continue;
                    }

                    var bestFutureRank = GetBestRankFromDropsList(futureDrops);
                    if (bestFutureRank > bestRank)
                    {
                        bestRank = bestFutureRank;
                        bestDrop = drop;
                    }
                }
            }
            else
            {
                foreach (TetrisDrop drop in drops)
                {
                    var rank = GetRank(drop.Columns);

                    if (rank > bestRank)
                    {
                        bestRank = rank;
                        bestDrop = drop;
                    }
                }
            }

            if (bestRank == 0)
            {
                var random = new Random();
                bestDrop = drops[random.Next(drops.Count)];
            }

            return bestDrop;
        }

        private static List<TetrisDrop> GetPossibleDrops(int[] columns, List<Piece> piecesInOrder)
        {
            if (piecesInOrder.Count > 1)
            {
                var possibleDrops = new List<TetrisDrop>();

                List<TetrisDrop> futureDrops = piecesInOrder.First().Service.GetPossibleDrops(columns);
                foreach (var drop in futureDrops)
                {
                    possibleDrops.AddRange(GetPossibleDrops(drop.Columns, piecesInOrder.Skip(1).ToList()));
                }

                return possibleDrops;
            }
            else if (piecesInOrder.Count == 1)
            {
                return piecesInOrder.Single().Service.GetPossibleDrops(columns);
            }
            else
            {
                return new List<TetrisDrop>();
            }
        }

        private static float GetBestRankFromDropsList(List<TetrisDrop> drops)
        {
            float bestRank = -1;

            foreach (TetrisDrop drop in drops)
            {
                var rank = GetRank(drop.Columns);

                if (rank > bestRank)
                {
                    bestRank = rank;
                }
            }

            return bestRank;
        }

        private static float GetRank(int[] columns)
        {
            Span<int> stack = stackalloc[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 1; i < columns.Length; i++)
            {
                stack[i - 1] = columns[i] - columns[i - 1];

                if (stack[i - 1] > 4 || stack[i - 1] < -4)
                {
                    return 0;
                }
            }

            var rankableStack = TetrisStackHelper.CreateStack(stack[0], stack[1], stack[2], stack[3], stack[4], stack[5], stack[6], stack[7]);
            return Ranks.GetCurrentRank(rankableStack);
        }
    }
}