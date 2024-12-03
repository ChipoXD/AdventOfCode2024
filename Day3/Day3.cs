using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3
{
    public static class Day3
    {
        private static List<string> _data;
        private const string InputFilePath = "Day3/Input.txt";
        

        static Day3()
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
            const string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            Regex regex = new Regex(regexPattern);

            int solution = _data
                .SelectMany(line => regex.Matches(line).Cast<Match>())
                .Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

            Console.WriteLine($"Day 3 First Task Solution: {solution}");
        }

        public static void SolveSecond()
        {
            int solution = 0;
            bool calculateActive = true;
            const string doString = "do()";
            const string dontString = "don't()";
            const string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";
            Regex regex = new Regex(regexPattern);

            foreach (Match match in _data.SelectMany(line => regex.Matches(line).Cast<Match>()))
            {
                switch (match.Value)
                {
                    case doString:
                        calculateActive = true;
                        break;
                    case dontString:
                        calculateActive = false;
                        break;
                    default:
                    {
                        if (calculateActive) solution += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                        break;
                    }
                }
            }

            Console.WriteLine($"Day 3 Second Task Solution: {solution}");
        }
    }
}
