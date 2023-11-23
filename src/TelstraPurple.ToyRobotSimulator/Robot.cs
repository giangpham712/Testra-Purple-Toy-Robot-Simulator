using System;
using System.Diagnostics.CodeAnalysis;
using TelstraPurple.ToyRobotSimulator.Output;

namespace TelstraPurple.ToyRobotSimulator
{
    public class Robot
    {
        private readonly IOutputWriter _outputWriter;

        private Position _position;
        private Direction _direction;
        private ITable _table;

        public Position Position => _position;

        public ITable Table => _table;

        public Direction Direction => _direction;

        public Robot(IOutputWriter outputWriter)
        {
            _outputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
        }

        public void Place(ITable table, Position position, Direction? direction)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (position == null)
                throw new ArgumentNullException(nameof(position));

            if (!table.IsValidPosition(position.X, position.Y))
                return;
            
            if (!IsPlaced && direction == null)
                return;

            _direction = IsPlaced ? _direction : direction.Value;
            _table = table;
            _position = position;
        }

        public void Move(int steps)
        {
            if (!IsPlaced)
                return;

            var nextPosition = CalculateNextPosition(steps);
            if (!_table.IsValidPosition(nextPosition.X, nextPosition.Y))
                return;

            _position = nextPosition;
        }

        public void Report()
        {
            if (!IsPlaced)
                return;

            _outputWriter.WriteLine($"{_position.X},{_position.Y},{_direction}");
        }

        public void RotateLeft()
        {
            if (!IsPlaced)
                return;

            _direction = (Direction)(((int)_direction + 3) % 4);
        }

        public void RotateRight()
        {
            if (!IsPlaced)
                return;

            _direction = (Direction)(((int)_direction + 1) % 4);
        }

        public bool IsPlaced => _table != null && _position != null;

        private Position CalculateNextPosition(int steps)
        {
            switch (_direction)
            {
                case Direction.NORTH:
                    return new Position(_position.X, _position.Y + steps);
                case Direction.EAST:
                    return new Position(_position.X + steps, _position.Y);
                case Direction.SOUTH:
                    return new Position(_position.X, _position.Y - steps);
                case Direction.WEST:
                    return new Position(_position.X - steps, _position.Y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(_direction), "Invalid direction.");
            }
        }
    }
}
