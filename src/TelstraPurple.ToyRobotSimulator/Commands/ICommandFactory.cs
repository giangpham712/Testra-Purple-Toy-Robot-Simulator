namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public interface ICommandFactory
    {
        IRobotCommand CreateCommand(string commandStr, Robot robot, ITable table);
    }
}