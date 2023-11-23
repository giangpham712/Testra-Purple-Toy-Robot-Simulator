using System;

namespace TelstraPurple.ToyRobotSimulator.Output
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
