using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Moq;
using TelstraPurple.ToyRobotSimulator.Commands;
using TelstraPurple.ToyRobotSimulator.Output;
using Xunit;

namespace TelstraPurple.ToyRobotSimulator.Tests.IntegrationTests
{
    public class SimulatorTests
    {
        [Theory]
        [ClassData(typeof(TestCases1Data))]
        public void TestCases1(int columns, int rows, string[] commands, string[] expectedOutputs)
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var table = new Table(columns, rows);
            var outputWriter = new ConsoleOutputWriter();
            var robot = new Robot(outputWriter);

            var robotCommandCreator = new RobotCommandFactory();
            var simulator = new Simulator(table, robot, robotCommandCreator);

            simulator.ProcessCommands(commands);

            var reports = output.ToString().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expectedOutputs, reports);
        }

        #region Sample test cases

        public class TestCases1Data : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    5,
                    5,
                    new [] { "PLACE 0,0,NORTH", "MOVE", "REPORT" },
                    new [] { "0,1,NORTH" }
                };

                yield return new object[]
                {
                    5,
                    5,
                    new [] { "PLACE 0,0,NORTH", "LEFT", "REPORT" },
                    new [] { "0,0,WEST" }
                };

                yield return new object[]
                {
                    5,
                    5,
                    new [] { "PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE", "REPORT" },
                    new [] { "3,3,NORTH" }
                };

                yield return new object[]
                {
                    5,
                    5,
                    new [] { "PLACE 1,2,EAST", "MOVE", "LEFT", "MOVE", "PLACE 3,1", "MOVE", "REPORT" },
                    new [] { "3,2,NORTH" }
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
