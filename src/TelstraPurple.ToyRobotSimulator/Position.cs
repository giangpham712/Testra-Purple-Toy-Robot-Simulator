using System;

namespace TelstraPurple.ToyRobotSimulator
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                var p = (Position) obj;
                return X == p.X && Y == p.Y;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}