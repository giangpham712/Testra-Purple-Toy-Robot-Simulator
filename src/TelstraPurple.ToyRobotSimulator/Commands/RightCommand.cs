namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class RightCommand : RobotCommand
    {
        public RightCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            _robot.RotateRight();
        }
    }
}
