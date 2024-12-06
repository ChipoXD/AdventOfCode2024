using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AdventOfCode2024.Day6
{
    public static class Day6
    {
        private static List<string> _data;
        private const string InputFilePath = "Day6/Input.txt";

        static Day6()
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
            Console.WriteLine($"Day 6 First Task Solution: {map.SolveDistinctGuardPositions()}");
        }

        public static void SolveSecond()
        {
            int solution = 0;
            Map map = new Map(_data);
            (int width, int height) = map.GetSize();

            (int startX, int startY) = map.InitialGuardPosition;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char currentContent = map.GetContent((x, y));
                    if ((x, y) == (startX, startY)) continue;
                    if (currentContent == '#') continue;
                    if (currentContent != '.') continue;
                    map.PlaceObstacle((x, y));

                    if (map.IsTimeLoop())
                    {
                        solution++;
                    }

                    map.ResetMap();

                }
            }

            Console.WriteLine($"Day 6 Second Task Solution: {solution}");
        }

        public static void SolveSecondParallelized()
        {
            Map baseMap = new Map(_data);
            (int width, int height) = baseMap.GetSize();

            (int startX, int startY) = baseMap.InitialGuardPosition;

            var candidates = new List<(int x, int y)>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char currentContent = baseMap.GetContent((x, y));
                    if ((x, y) == (startX, startY)) continue;
                    if (currentContent == '#') continue;
                    if (currentContent != '.') continue;

                    candidates.Add((x, y));
                }
            }

            int solution = 0;
            Parallel.ForEach(candidates, candidate =>
            {
                Map map = new Map(_data);
                map.PlaceObstacle(candidate);
                if (map.IsTimeLoop()) System.Threading.Interlocked.Increment(ref solution);
            });

            Console.WriteLine($"Day 6 Second Task Solution(para): {solution}");
        }
    }
}