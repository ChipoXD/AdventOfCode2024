using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AdventOfCode2024.Day18
{
    public static class Day18
    {
        private static List<string> _data;
        private const string InputFilePath = "Day18/Input.txt";

        static Day18()
        {
            LoadInput(InputFilePath);
        }

        private static void LoadInput(string inputFilePath)
        {
            try
            {
                _data = File.ReadAllLines(inputFilePath).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading input file: {ex.Message}");
                throw;
            }
        }


        public static void SolveFirst()
        {
            Map map = new Map(_data);
            List<Move> currentMoves = new List<Move>();
            Move winningMove;

            Move startingMove = new Move((0,0));
            currentMoves.Add(startingMove);


            while (true)
            {
                List<Move> newCurrentMoves = new List<Move>();
                foreach (Move move in currentMoves)
                {
                    newCurrentMoves.AddRange(move.CreateNewMoves(map));
                }

                foreach (Move move in newCurrentMoves)
                {
                    if (move.Coordinate == (71, 71))
                    {
                        winningMove = move;
                        break;
                    }
                }
                currentMoves = newCurrentMoves;
            }

            Console.WriteLine($"Day 2 First Task Solution: {winningMove.Turn}");
        }
    }
}
