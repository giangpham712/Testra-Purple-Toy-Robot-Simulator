using System.Collections;
using System.Collections.Generic;
using Moq;
using TelstraPurple.ToyRobotSimulator.Commands;
using TelstraPurple.ToyRobotSimulator.Output;
using Xunit;

namespace TelstraPurple.ToyRobotSimulator.Tests.UnitTests
{
    public class RobotCommandFactoryTests
    {
        private static Robot _robot;
        private static Table _table;

        static RobotCommandFactoryTests()
        {
            var outputWriterMock = new Mock<IOutputWriter>();
            _robot = new Robot(outputWriterMock.Object);
            _table = new Table(5, 5);
        }

        [Theory]
        [ClassData(typeof(ValidCommandTestData))]
        public void CreateCommand_ValidInput_ReturnsCommand(string commandStr, IRobotCommand expectedRobotCommand)
        {
            var factory = new RobotCommandFactory();
            var command = factory.CreateCommand(commandStr, _robot, _table);

            Assert.Equal(expectedRobotCommand.GetType(), command.GetType());
            Assert.Equal(expectedRobotCommand.Robot, command.Robot);

            if (expectedRobotCommand.GetType() == command.GetType() && command is PlaceCommand placeCommand)
            {
                var expectedPlaceCommand = (PlaceCommand)expectedRobotCommand;
                Assert.Equal(expectedPlaceCommand.Table, placeCommand.Table);
                Assert.Equal(expectedPlaceCommand.Position, placeCommand.Position);
            }
        }

        [Theory]
        [ClassData(typeof(InvalidCommandTestData))]
        public void CreateCommand_InvalidInput_ThrowsException(string commandStr)
        {
            var factory = new RobotCommandFactory();
            Assert.Throws<InvalidCommandException>(() => factory.CreateCommand(commandStr, _robot, _table));
        }

        #region command test cases

        public class ValidCommandTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    "PLACE 0,0,NORTH",
                    new PlaceCommand(_robot, _table, new Position(0, 0),  Direction.NORTH),
                };

                yield return new object[]
                {
                    "PLACE 10,20,SOUTH",
                    new PlaceCommand(_robot, _table, new Position(10, 20),  Direction.SOUTH),
                };

                yield return new object[]
                {
                    "PLACE 1,20,WEST",
                    new PlaceCommand(_robot, _table, new Position(1, 20),  Direction.WEST),
                };

                yield return new object[]
                {
                    "PLACE 1,2,EAST",
                    new PlaceCommand(_robot, _table, new Position(1, 2),  Direction.EAST),
                };

                yield return new object[]
                {
                    "REPORT",
                    new ReportCommand(_robot),
                };

                yield return new object[]
                {
                    "LEFT",
                    new LeftCommand(_robot),
                };

                yield return new object[]
                {
                    "RIGHT",
                    new RightCommand(_robot),
                };

                yield return new object[]
                {
                    "MOVE",
                    new MoveCommand(_robot, 1),
                };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion

        #region Invalid command test cases

        public class InvalidCommandTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    "ABC"
                };

                yield return new object[]
                {
                    "LEFT 123"
                };

                yield return new object[]
                {
                    "MOVE 12"
                };

                yield return new object[]
                {
                    "PLACE 1,2,ABC"
                };

                yield return new object[]
                {
                    "PLACE 1,A,EAST"
                };

                yield return new object[]
                {
                    ""
                };

                yield return new object[]
                {
                    "1,2,NORTH"
                };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion
    }
}
