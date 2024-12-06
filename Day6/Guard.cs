using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day6
{
    public class Guard
    {
        public Direction Direction;
        public (int, int) Position;

        public Guard(Direction direction, (int, int) position)
        {
            Direction = direction;
            Position = position;
        }

        public void Turn()
        {
            switch (Direction)
            {
                case Direction.Right:
                    Direction = Direction.Down;
                    break;
                case Direction.Left:
                    Direction = Direction.Up;
                    break;
                case Direction.Up:
                    Direction = Direction.Right;
                    break;
                case Direction.Down:
                    Direction = Direction.Left;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Move()
        {
            Position = GetNextPosition();
        }

        public (int, int) GetNextPosition()
        {
            switch (Direction)
            {
                case Direction.Right:
                    return (Position.Item1 + 1, Position.Item2);
                case Direction.Left:
                    return (Position.Item1 - 1, Position.Item2); ;
                case Direction.Up:
                    return (Position.Item1, Position.Item2 - 1); ;
                case Direction.Down:
                    return (Position.Item1, Position.Item2 + 1); ;
                default:
                    throw new ArgumentOutOfRangeException();
            }
                
        }
    }
    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    };
}
