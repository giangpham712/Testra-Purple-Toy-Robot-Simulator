using System;

namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class InvalidCommandException : Exception
    {
        private readonly string _command;

        public string Command => _command;

        public InvalidCommandException(string command) : base("Invalid command.")
        {
            _command = command;
        }
    }
}
