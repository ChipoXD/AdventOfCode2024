using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Day2
{
    public static class Day2
    {
        private static List<List<int>> _data;
        private const string InputFilePath = "Day2/Input.txt";
        private const int MinJump = 1;
        private const int MaxJump = 3;

        static Day2()
        {
            LoadInput(InputFilePath);
        }

        private static void LoadInput(string inputFilePath)
        {
            const char delimiter = ' ';
            _data = new List<List<int>>();
            try
            {
                string[] lines = File.ReadAllLines(inputFilePath);
                foreach (string line in lines)
                {
                    try
                    {
                        List<int> row = line.Split(delimiter)
                            .Where(part => !string.IsNullOrWhiteSpace(part))
                            .Select(int.Parse)
                            .ToList();
                        _data.Add(row);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Invalid row in input file: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading input file: {ex.Message}");
                throw;
            }
        }

        private static bool IsSafeJump(List<int> levels)
        {
            if (levels.Count < 2)       return false;
            if (levels[0] == levels[1]) return false;

            bool isDesc = levels[1] - levels[0] < 0;
            for (int n = 1; n < levels.Count; n++)
            {
                int jump = levels[n] - levels[n - 1];
                if ((jump > 0 && isDesc) || (jump < 0 && !isDesc))        return false;
                if (Math.Abs(jump) < MinJump || Math.Abs(jump) > MaxJump) return false;
            }
            return true;
        }

        private static bool IsPossibleSafeJump(List<int> levels)
        {
            if (IsSafeJump(levels)) return true;
            for (int n = 0; n < levels.Count; n++)
            {
                // TODO: Optimize this to avoid list copying 
                List<int> tempLevels = new List<int>(levels);
                tempLevels.RemoveAt(n); 
                if (IsSafeJump(tempLevels)) return true;
            }

            return false;
        }


        public static void SolveFirst()
        {
            int solution = _data.Count(IsSafeJump);
            Console.WriteLine($"Day 2 First Task Solution: {solution}");
        }

        public static void SolveSecond()
        {
            int solution = _data.Count(IsPossibleSafeJump);
            Console.WriteLine($"Day 2 Second Task Solution: {solution}");
        }
    }
}
