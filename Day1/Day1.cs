using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024.Day1
{
    public static class Day1
    {
        private static List<int> _firstList;
        private static List<int> _secondList;
        private const string InputFilePath = "Day1/Input.txt";

        static Day1()
        {
            LoadInput(InputFilePath);
        }

        private static void LoadInput(string inputFilePath)
        {
            _firstList = new List<int>();
            _secondList = new List<int>();
            const string delimiter = "   ";
            try
            {
                string[] lines = File.ReadAllLines(inputFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(new[] { delimiter }, StringSplitOptions.None);
                    if (parts.Length != 2 || !int.TryParse(parts[0], out int firstValue) || !int.TryParse(parts[1], out int secondValue))
                    {
                        throw new FormatException($"Invalid line format: {line}");
                    }
                    _firstList.Add(Convert.ToInt32(firstValue));
                    _secondList.Add(Convert.ToInt32(secondValue));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading input file: {ex.Message}");
                throw;
            }
        }

            public static void SolveFirst()
        {
            _firstList.Sort();
            _secondList.Sort();
            int solution = _firstList.Select((t, n) => Math.Abs(t - _secondList[n])).Sum();

            Console.WriteLine($"Day 1 First Task Solution: {solution}");
        }

        public static void SolveSecond()
        {
            int solution = _firstList.Sum(n => _secondList.Count(m => m == n) * n);

            Console.WriteLine($"Day 1 Second Task Solution: {solution}");
        }
    }
}
