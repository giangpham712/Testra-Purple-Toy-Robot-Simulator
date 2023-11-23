namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class LeftCommand : RobotCommand
    {
        public LeftCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            _robot.RotateLeft();
        }
    }
}
