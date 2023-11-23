namespace TelstraPurple.ToyRobotSimulator.Commands
{
    public class PlaceCommand : RobotCommand
    {
        private ITable _table;
        private Position _position;
        private Direction? _direction;

        public ITable Table => _table;
        public Position Position => _position;
        public Direction? Direction => _direction;

        public PlaceCommand(Robot robot, ITable table, Position position, Direction? direction) : base(robot)
        {
            _table = table;
            _position = position;
            _direction = direction;
        }
        
        public override void Execute()
        {
            _robot.Place(_table, _position, _direction);
        }
    }
}
