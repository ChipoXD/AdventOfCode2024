using System;
using System.Collections.Generic;

namespace AdventOfCode2024.Day18
{
    public class Map
    {
        private const int Size = 71;
        private readonly int[,] _mapContent;

        public Map(List<string> obstacles)
        {
            _mapContent = new int[Size, Size];
            for (int i = 0; i < obstacles.Count; i++)
            {
                var (x, y) = ParseCoordinates(obstacles[i]);
                _mapContent[x, y] = i;
            }
        }
        
        public bool AvailableSpaceAtTurn((int, int) coordinate, int turn)
        {

            return _mapContent[coordinate.Item1,coordinate.Item2] <= turn;
        }

        public bool IsCoordinateInsideMap((int, int) coordinate)
        {
            return coordinate.Item1 >= 0 &&
                   coordinate.Item1 < Size &&
                   coordinate.Item2 >= 0 &&
                   coordinate.Item2 < Size;
        }

        private static (int x, int y) ParseCoordinates(string coordinateString)
        {
            var parts = coordinateString.Split(',');
            return (Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
        }
    }
}
