namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class ReportCommand : RobotCommand
    {
        public ReportCommand(Robot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            _robot.Report();
        }
    }
}
