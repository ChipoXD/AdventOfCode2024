using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day4
{
    public static class Day4
    {
        private static List<string> _data;
        private const string InputFilePath = "Day4/Input.txt";
        static Day4()
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

        private static List<string> TransposeList(List<string> list)
        {
            List<string> newList = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                newList.Add("");
                for (int j = 0; j < list.Count; j++)
                {
                    newList[i] += list[j][i];
                }
            }

            return newList;
        }

        private static List<string> ScewListRight(List<string> list)
        {
            List<string> newList = new List<string>();

            // Top left triangle + middle
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add("");
                for (int j = 0; j <= i; j++)
                {
                    newList[i] += list[i - j][j];
                }
            }

            // Bottom right triangle
            for (int i = 1; i < list.Count; i++)
            {
                newList.Add("");
                for (int j = 0; j < list.Count - i; j++)
                {
                    newList[list.Count + i - 1] += list[list.Count - 1 - j][j + i];
                }
            }

            return newList;
        }
        
        private static List<string> ScewListLeft(List<string> list)
        {
            List<string> newList = new List<string>();

            // Top right triangle + middle
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add("");
                for (int j = 0; j <= i; j++)
                {
                    newList[i] += list[i - j][list.Count() - j - 1];
                }
            }

            // Bottom left triangle
            for (int i = 1; i < list.Count; i++)
            {
                newList.Add("");
                for (int j = 0; j < list.Count - i; j++)
                {
                    newList[list.Count + i - 1] += list[list.Count - 1 - j][list.Count() - (j + i) - 1];
                }
            }
            //TODO returned list is backwards. Still works lol
            return newList;
        }

        public static void SolveFirst()
        {
            const string regexPattern = @"(?=(XMAS|SAMX))";
            Regex regex = new Regex(regexPattern);
            int solution = 0;

            solution +=                _data.Select(line => regex.Matches(line)).Select(matches => matches.Count).Sum();
            solution += TransposeList(_data).Select(line => regex.Matches(line)).Select(matches => matches.Count).Sum();
            solution += ScewListRight(_data).Select(line => regex.Matches(line)).Select(matches => matches.Count).Sum();
            solution +=  ScewListLeft(_data).Select(line => regex.Matches(line)).Select(matches => matches.Count).Sum();

            Console.WriteLine($"Day 4 First Task Solution: {solution}");
        }

        public static void SolveSecond()
        {
            string regexPattern = @"(?=(MAS|SAM))";
            int solution = 0;

            List<string> dataSL = ScewListLeft(_data);
            List<string> dataSR = ScewListRight(_data);

            

            List<(int, int)> indexesSL;
            List<(int, int)> indexesSR;

            foreach (string row in dataSL)
            {
                MatchCollection matchesSL = Regex.Matches(row, regexPattern);
                indexesSL.Add(  )
            }

            Console.WriteLine($"Day 4 Second Task Solution: {solution}");
        }
    }
}
