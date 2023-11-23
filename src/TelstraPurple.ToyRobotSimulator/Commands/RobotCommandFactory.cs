using System;
using System.Linq;

namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class RobotCommandFactory : ICommandFactory
    {
        public IRobotCommand CreateCommand(string commandStr, Robot robot, ITable table)
        {
            if (string.IsNullOrWhiteSpace(commandStr))
            {
                throw new InvalidCommandException(commandStr);
            }

            switch (commandStr)
            {
                case "MOVE":
                    return new MoveCommand(robot, 1); // Move robot 1 step by default
                case "LEFT":
                    return new LeftCommand(robot);
                case "RIGHT":
                    return new RightCommand(robot);
                case "REPORT":
                    return new ReportCommand(robot);
                default:
                    if (commandStr.StartsWith("PLACE "))
                    {
                        return CreatePlaceCommand(commandStr, robot, table);
                    }

                    break;
            }

            throw new InvalidCommandException(commandStr);
        }

        private IRobotCommand CreatePlaceCommand(string commandStr, Robot robot, ITable table)
        {
            var commandParts = commandStr.Split(' ');
            var command = commandParts.First();

            if (command != "PLACE" || commandParts.Length != 2)
            {
                throw new InvalidCommandException(commandStr);
            }

            var commandParams = commandParts.Last().Split(',');

            if (commandParams.Length != 2 && commandParams.Length != 3)
            {
                throw new InvalidCommandException(commandStr);
            }

            if (!int.TryParse(commandParams[0], out var x) ||
                !int.TryParse(commandParams[1], out var y))
            {
                throw new InvalidCommandException(commandStr);
            }

            Direction? direction = null;

            if (commandParams.Length == 3)
            {
                if (Enum.TryParse(commandParams[2], out Direction parsedDirection))
                {
                    direction = parsedDirection;
                }
                else
                {
                    throw new InvalidCommandException(commandStr);
                }
            }

            return new PlaceCommand(robot, table, new Position(x, y), direction);
        }
    }
}
