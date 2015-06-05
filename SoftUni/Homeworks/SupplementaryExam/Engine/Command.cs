using System;

namespace IssueTracker.Engine
{
    using Interfaces.Engine;
    using System.Collections.Generic;

    public class Command : ICommand
    {
        private const char CommandNameSeparator = '?';
        private const char CommandParameterSeparator = '&';
        private const char CommandValueSeparator = '=';

        private string name;
        private IDictionary<string, string> parameters = new Dictionary<string, string>();

        public Command(string input)
        {
            this.TranslateInput(input);
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Command name is reqired");
                }

                this.name = value;
            }
        }

        public static Command Parse(string input)
        {
            return new Command(input);
        }

        public IDictionary<string, string> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }

        private void TranslateInput(string input)
        {
            if (input.IndexOf(CommandNameSeparator) > -1)
            {
                int parametersBeginning = input.IndexOf(CommandNameSeparator);

                this.Name = input.Substring(0, parametersBeginning);

                var parametersKeysAndValues = input.Substring(parametersBeginning + 1,
                    input.Length - parametersBeginning - 1)
                    .Split(new[] {CommandParameterSeparator}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var parameter in parametersKeysAndValues)
                {
                    var split = parameter.Split(new[] {CommandValueSeparator}, StringSplitOptions.RemoveEmptyEntries);
                    this.Parameters.Add(split[0], split[1]);
                }
            }
            else
            {
                this.Name = input;
            }
        }
    }
}
