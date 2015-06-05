namespace IssueTracker.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Interfaces.Engine;

    public class ConsoleInterface : IUserInterface
    {
        public IEnumerable<string> Input()
        {
            string currentLine = Console.ReadLine();
            while (currentLine != null)
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
            var result = new StringBuilder();
            foreach (string line in output)
            {
                result.AppendLine(line.Trim());
            }

            Console.WriteLine(result.ToString().Trim());
        }
    }
}
