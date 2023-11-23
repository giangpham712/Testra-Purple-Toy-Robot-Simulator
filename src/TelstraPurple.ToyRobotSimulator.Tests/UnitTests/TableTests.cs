using System;
using Xunit;

namespace TelstraPurple.ToyRobotSimulator.Tests.UnitTests
{
    public class TableTests
    {
        [Theory]
        [InlineData(1, 3)]
        [InlineData(5, 5)]
        [InlineData(3, 105)]
        public void Init_PositiveNumbersOfRowsOrColumns_ReturnsTable(int columns, int rows)
        {
            var table = new Table(columns, rows);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(50, -5)]
        [InlineData(-1, 105)]
        [InlineData(20, 0)]
        public void Init_NonPositiveNumbersOfRowsOrColumns_ThrowsException(int columns, int rows)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Table(columns, rows));
            Assert.Equal("Numbers of rows and columns must be greater than zero.", exception.Message);
        }

        [Theory]
        [InlineData(5, 5, 1, 2)]
        [InlineData(5, 5, 4, 4)]
        [InlineData(6, 6, 4, 3)]
        [InlineData(6, 6, 5, 5)]
        public void IsValidPosition_ValidPosition_ReturnsTrue(int columns, int rows, int x, int y)
        {
            var table = new Table(columns, rows);
            var isValid = table.IsValidPosition(x, y);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(5, 5, 5, 4)]
        [InlineData(5, 2, 4, 4)]
        [InlineData(3, 3, -1, 1)]
        [InlineData(2, 2, -3, 0)]
        public void IsValidPosition_InvalidPosition_ReturnsFalse(int columns, int rows, int x, int y)
        {
            var table = new Table(columns, rows);
            var isValid = table.IsValidPosition(x, y);
            Assert.False(isValid);
        }
    }
}
