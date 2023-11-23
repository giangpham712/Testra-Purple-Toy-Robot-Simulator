using System.Linq;
using TelstraPurple.ToyRobotSimulator.Commands;

namespace TelstraPurple.ToyRobotSimulator
{
    public class Simulator
    {
        private ITable _table;
        private Robot _robot;

        private ICommandFactory _commandFactory;

        public Simulator(ITable table, Robot robot, ICommandFactory commandFactory)
        {
            _table = table;
            _robot = robot;

            _commandFactory = commandFactory;
        }

        public void ProcessCommands(string[] commands)
        {
            if (commands == null || !commands.Any())
                return;

            foreach (var commandStr in commands)
            {
                try
                {
                    var command = _commandFactory.CreateCommand(commandStr, _robot, _table);
                    command.Execute();
                }
                catch (InvalidCommandException)
                {
                    // Ignore if command is invalid
                }
            }
        }
    }
}
