namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public interface IRobotCommand
    {
        Robot Robot { get; }

        void Execute();
    }
}
