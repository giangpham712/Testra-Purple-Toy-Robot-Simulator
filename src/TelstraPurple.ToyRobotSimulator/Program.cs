using System;
using System.Linq;
using TelstraPurple.ToyRobotSimulator.Commands;
using TelstraPurple.ToyRobotSimulator.Output;

namespace TelstraPurple.ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    throw new ArgumentException("Invalid arguments for the console app.");
                }

                var tableDimensions = args[0].Split(',');

                if (tableDimensions.Length != 2 ||
                    !int.TryParse(tableDimensions[0], out var columns) ||
                    !int.TryParse(tableDimensions[1], out var rows))
                {
                    throw new ArgumentException("Invalid arguments for the console app.");
                }

                var table = new Table(columns, rows);

                var consoleOutputWriter = new ConsoleOutputWriter();
                var robot = new Robot(consoleOutputWriter);

                var robotCommandCreator = new RobotCommandFactory();
                var simulator = new Simulator(table, robot, robotCommandCreator);
                simulator.ProcessCommands(args.Skip(1).ToArray());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
