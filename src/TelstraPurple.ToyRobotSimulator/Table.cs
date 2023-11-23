using System;

namespace TelstraPurple.ToyRobotSimulator
{
    public class Table : ITable
    {
        private readonly int _rows;
        private readonly int _columns;

        public Table(int columns, int rows)
        {
            if (columns < 1 || rows < 1)
            {
                throw new ArgumentException("Numbers of rows and columns must be greater than zero.");
            }

            _columns = columns;
            _rows = rows;
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _columns &&
                   y >= 0 && y < _rows;
        }
    }
}
