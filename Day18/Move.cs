using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day18
{
    internal class Move
    {
        private readonly Move _previousMove;
        public readonly (int, int) Coordinate;
        private const int MagicNumber = 1024;
        public int Turn;

        public Move((int,int) coordinate, int turn = 0, Move previousMove = null)
        {
            _previousMove = previousMove;
            Coordinate = coordinate;
            Turn = turn;
        }

        private List<(int, int)> GetAvailableNewMoves(Map map)
        {
            List<(int, int)> availableMoves = new List<(int, int)> ();
            
            // Move right
            (int, int) moveRightCoordinate = (Coordinate.Item1, Coordinate.Item2 + 1);
            if (map.IsCoordinateInsideMap(moveRightCoordinate) && map.AvailableSpaceAtTurn(moveRightCoordinate,MagicNumber)) availableMoves.Add(moveRightCoordinate);

            // Move Up
            (int, int) moveUpCoordinate    = (Coordinate.Item1 - 1, Coordinate.Item2);
            if (map.IsCoordinateInsideMap(moveUpCoordinate) && map.AvailableSpaceAtTurn(moveUpCoordinate, MagicNumber)) availableMoves.Add(moveUpCoordinate);

            // Move Left
            (int, int) moveLeftCoordinate  = (Coordinate.Item1, Coordinate.Item2 - 1);
            if (map.IsCoordinateInsideMap(moveLeftCoordinate) && map.AvailableSpaceAtTurn(moveLeftCoordinate, MagicNumber)) availableMoves.Add(moveLeftCoordinate);

            // Move Down
            (int, int) moveDownCoordinate  = (Coordinate.Item1 + 1, Coordinate.Item2);
            if (map.IsCoordinateInsideMap(moveDownCoordinate) && map.AvailableSpaceAtTurn(moveDownCoordinate, MagicNumber)) availableMoves.Add(moveDownCoordinate);

            return availableMoves;
        }

        public List<Move> CreateNewMoves(Map map)
        {
            List<(int, int)> availableMoves = GetAvailableNewMoves(map);
            return availableMoves.Select(coordinate => new Move(coordinate, Turn + 1, this)).ToList();
        }
    }
}
