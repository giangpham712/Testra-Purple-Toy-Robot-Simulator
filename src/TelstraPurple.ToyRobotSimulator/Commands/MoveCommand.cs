namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class MoveCommand : RobotCommand
    {
        private int _steps;
        public MoveCommand(Robot robot, int steps) : base(robot)
        {
            _steps = steps;
        }

        public override void Execute()
        {
            _robot.Move(_steps);
        }
    }
}
