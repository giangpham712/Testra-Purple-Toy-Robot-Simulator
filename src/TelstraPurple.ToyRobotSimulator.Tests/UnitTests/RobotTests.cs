using System;
using Moq;
using TelstraPurple.ToyRobotSimulator.Output;
using Xunit;

namespace TelstraPurple.ToyRobotSimulator.Tests.UnitTests
{
    public class RobotTests
    {
        [Fact]
        public void Init_OutputWriterIsNotNull_Returns()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
        }

        [Fact]
        public void Init_OutputWriterIsNull_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Robot(null));
            Assert.Equal("Value cannot be null. (Parameter 'outputWriter')", exception.Message);
        }

        [Fact]
        public void Place_TableIsNull_ThrowsException()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var exception = Assert.Throws<ArgumentNullException>(() => robot.Place(null, new Position(0, 0), null));
            Assert.Equal("Value cannot be null. (Parameter 'table')", exception.Message);
        }

        [Fact]
        public void Place_PositionIsNull_ThrowsException()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var exception = Assert.Throws<ArgumentNullException>(() => robot.Place(new Table(1, 1), null, null));
            Assert.Equal("Value cannot be null. (Parameter 'position')", exception.Message);
        }

        [Fact]
        public void Place_RobotIsNotPlacedAndPositionIsInvalid_RobotIsNotPlaced()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var initialDirection = robot.Direction;

            robot.Place(new Table(5, 5), new Position(6, 1), Direction.SOUTH);

            Assert.Equal(initialDirection, robot.Direction);
            Assert.False(robot.IsPlaced);
        }

        [Fact]
        public void Place_RobotIsNotPlacedAndDirectionIsNull_RobotIsNotPlaced()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var initialDirection = robot.Direction;

            robot.Place(new Table(5, 5), new Position(3, 2), null);

            Assert.Equal(initialDirection, robot.Direction);
            Assert.False(robot.IsPlaced);
        }

        [Fact]
        public void Place_RobotIsNotPlacedAndValidCommand_RobotIsPlaced()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            robot.Place(new Table(5, 5), new Position(3, 2), Direction.EAST);

            Assert.Equal(Direction.EAST, robot.Direction);
            Assert.True(robot.IsPlaced);
            Assert.Equal(3, robot.Position.X);
            Assert.Equal(2, robot.Position.Y);
        }

        [Fact]
        public void Place_RobotIsPlacedAndValidCommand_RobotIsPlacedToNewPositionWithOldDirection()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            robot.Place(new Table(5, 5), new Position(3, 2), Direction.EAST);

            robot.Place(new Table(5, 5), new Position(1, 4), Direction.SOUTH);

            Assert.Equal(1, robot.Position.X);
            Assert.Equal(4, robot.Position.Y);
            Assert.Equal(Direction.EAST, robot.Direction);
        }

        [Fact]
        public void Place_RobotIsPlacedAndPositionIsInvalid_RobotStaysAtCurrentPosition()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            robot.Place(new Table(5, 5), new Position(2, 3), Direction.SOUTH);

            robot.Place(new Table(5, 5), new Position(6, 1), Direction.SOUTH);

            Assert.Equal(2, robot.Position.X);
            Assert.Equal(3, robot.Position.Y);
            Assert.Equal(Direction.SOUTH, robot.Direction);
        }

        [Fact]
        public void RotateLeft_RobotIsNotPlaced_Returns()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var initialDirection = robot.Direction;

            robot.RotateLeft();
            Assert.False(robot.IsPlaced);
            Assert.Equal(initialDirection, robot.Direction);
        }

        [Fact]
        public void RotateRight_RobotIsNotPlaced_Returns()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var initialDirection = robot.Direction;

            robot.RotateLeft();
            Assert.False(robot.IsPlaced);
            Assert.Equal(initialDirection, robot.Direction);
        }

        [Theory]
        [InlineData(Direction.EAST, Direction.NORTH)]
        [InlineData(Direction.NORTH, Direction.WEST)]
        [InlineData(Direction.WEST, Direction.SOUTH)]
        [InlineData(Direction.SOUTH, Direction.EAST)]
        public void RotateLeft_RobotIsPlaced_RobotRotatesToExpectedDirection(Direction initialDirection, Direction expectedNewDirection)
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            robot.Place(new Table(5, 5), new Position(2, 3), initialDirection);
            robot.RotateLeft();

            Assert.Equal(expectedNewDirection, robot.Direction);
        }

        [Theory]
        [InlineData(Direction.EAST, Direction.SOUTH)]
        [InlineData(Direction.NORTH, Direction.EAST)]
        [InlineData(Direction.WEST, Direction.NORTH)]
        [InlineData(Direction.SOUTH, Direction.WEST)]
        public void RotateRight_RobotIsPlaced_RobotRotatesToExpectedDirection(Direction initialDirection, Direction expectedNewDirection)
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            robot.Place(new Table(5, 5), new Position(2, 3), initialDirection);
            robot.RotateRight();

            Assert.Equal(expectedNewDirection, robot.Direction);
        }

        [Fact]
        public void Move_RobotIsNotPlaced_Returns()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);
            var initialDirection = robot.Direction;

            robot.Move(1);
            Assert.False(robot.IsPlaced);
            Assert.Equal(initialDirection, robot.Direction);
        }

        [Theory]
        [InlineData(2, 2, Direction.SOUTH, 1, 2, 1)]
        [InlineData(2, 2, Direction.NORTH, 1, 2, 3)]
        [InlineData(2, 2, Direction.EAST, 1, 3, 2)]
        [InlineData(2, 2, Direction.WEST, 1, 1, 2)]
        public void Move_RobotIsPlacedAndValidMove_RobotMovesToExpectedPosition(
            int initialX, int initialY, Direction initialDirection, int steps, 
            int expectedNewX, int expectedNewY)
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            var tableMock = new Mock<ITable>();
            tableMock.Setup(table => table.IsValidPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            robot.Place(tableMock.Object, new Position(initialX, initialY), initialDirection);

            robot.Move(1);

            Assert.Equal(expectedNewX, robot.Position.X);
            Assert.Equal(expectedNewY, robot.Position.Y);
            Assert.Equal(initialDirection, robot.Direction);
        }

        [Theory]
        [InlineData(2, 2, Direction.SOUTH, 1)]
        [InlineData(2, 2, Direction.NORTH, 1)]
        [InlineData(2, 2, Direction.EAST, 1)]
        [InlineData(2, 2, Direction.WEST, 1)]
        [InlineData(3, 5, Direction.SOUTH, 2)]
        [InlineData(2, 5, Direction.EAST, 5)]
        [InlineData(1, 10, Direction.WEST, 11)]
        public void Move_RobotIsPlacedAndInvalidMove_RobotStaysAtCurrentPosition(
            int initialX, int initialY, Direction initialDirection, int steps)
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            var robot = new Robot(outputWriterMock.Object);

            var tableMock = new Mock<ITable>();
            tableMock.Setup(table => table.IsValidPosition(It.IsIn(initialX), It.IsIn(initialY))).Returns(true);
            tableMock.Setup(table => table.IsValidPosition(It.IsNotIn(initialX), It.IsNotIn(initialY))).Returns(false);

            robot.Place(tableMock.Object, new Position(initialX, initialY), initialDirection);

            robot.Move(1);

            Assert.Equal(initialX, robot.Position.X);
            Assert.Equal(initialY, robot.Position.Y);
            Assert.Equal(initialDirection, robot.Direction);
        }
    }
}
