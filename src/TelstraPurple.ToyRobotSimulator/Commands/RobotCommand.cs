using System;
using System.Diagnostics.CodeAnalysis;

namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public abstract class RobotCommand : IRobotCommand
    {
        protected Robot _robot;

        public Robot Robot => _robot;

        protected RobotCommand(Robot robot)
        {
            _robot = robot ?? throw new ArgumentNullException(nameof(robot));
        }

        public abstract void Execute();
    }
}
