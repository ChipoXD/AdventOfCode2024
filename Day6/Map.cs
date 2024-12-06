using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Day6
{
    public class Map
    {
        private List<char[]> _mapContent;
        private const char ObstacleChar = '#';
        private const char GuardChar = '^';
        private const char NotVisitedChar = '.';
        private const char VisitedChar = 'X';
        public Guard Guard;
        public (int, int) InitialGuardPosition;
        private readonly List<string> _originalMapContent;

        public Map(List<string> mapContent)
        {
            _originalMapContent = mapContent;
            Initialize(mapContent);
        }
        public void ResetMap()
        {
            Initialize(_originalMapContent);
        }

        public void PrintMap()
        {
            foreach (var line in _mapContent)
            {
                Console.WriteLine(line);
            }
        }

        private void Initialize(List<string> mapContent)
        {
            _mapContent = new List<char[]>(mapContent.Count);
            foreach (var line in mapContent)
            {
                _mapContent.Add(line.ToCharArray());
            }

            InitialGuardPosition = GetGuardStartingPosition();
            Guard = new Guard(Direction.Up, InitialGuardPosition);
        }

        public (int, int) GetSize()
        {
            return (_mapContent[0].Length, _mapContent.Count);
        }

        private bool IsCoordinateInsideMap((int, int) coordinate)
        {
            var mapSize = GetSize();
            return coordinate.Item1 >= 0 &&
                   coordinate.Item1 < mapSize.Item1 &&
                   coordinate.Item2 >= 0 &&
                   coordinate.Item2 < mapSize.Item2;
        }

        public char GetContent((int, int) coordinate)
        {
            if (!IsCoordinateInsideMap(coordinate))
                throw new OutsideRangeException(
                    $"Coordinates ({coordinate.Item1}, {coordinate.Item2}) are outside the map range.");
            return _mapContent[coordinate.Item2][coordinate.Item1];
        }

        public void PlaceObstacle((int, int) coordinate)
        {
            if (!IsCoordinateInsideMap(coordinate))
                throw new OutsideRangeException(
                    $"Coordinates ({coordinate.Item1}, {coordinate.Item2}) are outside the map range.");
            _mapContent[coordinate.Item2][coordinate.Item1] = ObstacleChar;
        }

        private void MarkPositionAsVisited((int, int) coordinate)
        {
            if (!IsCoordinateInsideMap(coordinate))
                throw new OutsideRangeException(
                    $"Coordinates ({coordinate.Item1}, {coordinate.Item2}) are outside the map range.");
            _mapContent[coordinate.Item2][coordinate.Item1] = VisitedChar;
        }

        private (int, int) GetGuardStartingPosition()
        {
            for (int y = 0; y < _mapContent.Count; y++)
            {
                var line = _mapContent[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == GuardChar)
                    {
                        return (x, y);
                    }
                }
            }

            throw new Exception("Guard not found on the map.");
        }

        private int CountVisitedPositions()
        {
            return _mapContent.Sum(line => line.Count(c => c == VisitedChar));
        }

        public bool IsTimeLoop()
        {
            var visitedStates = new HashSet<(int x, int y, Direction d)>();

            while (true)
            {
                var state = (Guard.Position.Item1, Guard.Position.Item2, Guard.Direction);
                if (!visitedStates.Add(state))
                {
                    return true;
                }

                (int, int) guardNextPosition = Guard.GetNextPosition();

                if (!IsCoordinateInsideMap(guardNextPosition))
                    return false;

                if (GetContent(guardNextPosition) == ObstacleChar)
                {
                    Guard.Turn();
                }
                else
                {
                    MarkPositionAsVisited(Guard.Position);
                    Guard.Move();
                }
            }
        }

        public int SolveDistinctGuardPositions()
        {
            while (true)
            {
                (int, int) guardNextPosition = Guard.GetNextPosition();
                if (!IsCoordinateInsideMap(guardNextPosition)) break;
                if (GetContent(guardNextPosition) == ObstacleChar)
                {
                    Guard.Turn();
                }
                else
                {
                    MarkPositionAsVisited(Guard.Position);
                    Guard.Move();
                }
            }

            return
                CountVisitedPositions() + 1; // I'm not removing the guard, so +1 for the position the guard stands on
        }
    }
    public class OutsideRangeException : Exception
    {
        public OutsideRangeException(string message) : base(message)
        {
        }
    }
}
