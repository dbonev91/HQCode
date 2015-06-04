namespace VehiclePark.Engine
{
    using Interfaces.Engine;
    using System.Collections.Generic;
    using System;

    public class ConsoleInterface : IUserInterface
    {
        public IEnumerable<string> Input()
        {
            string currentLine = Console.ReadLine();

            while (Console.ReadLine() != null)
            {
                if (currentLine == string.Empty)
                {
                    currentLine = Console.ReadLine();
                    continue;
                }

                yield return currentLine;
                currentLine = Console.ReadLine();
            }
        }

        public void Output(IEnumerable<string> output)
        {
            throw new NotImplementedException();
        }
    }
}
