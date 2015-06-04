namespace VehiclePark.Engine
{
    using System;
    using Interfaces.Engine;
    using System.Collections.Generic;

    public class Command : ICommand
    {
        private string name;
        private IDictionary<string, string> parameters = new Dictionary<string, string>();

        private const char ParametersBegin = '{';
        private const char ParametersDelimiter = ',';
        private const string ParamKeyValueDelimiter = ": ";

        public Command(string command)
        {
            this.TranslateInput(command);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Command name is required!");
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
            get
            {
                return this.parameters;
            }
            private set
            {
                this.parameters = value;
            }
        }

        private void TranslateInput(string input)
        {
            int paramsBegin = input.IndexOf(ParametersBegin);

            string commandName = input.Substring(0, paramsBegin).Trim();
            this.Name = commandName;

            string parametersString = input.Substring(paramsBegin + 1, input.Length - paramsBegin - 2);

            var paramsCollection = parametersString.Split(new [] { ParametersDelimiter }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var paramKeyValue in paramsCollection)
            {
                var split = paramKeyValue.Split(new[] { ParamKeyValueDelimiter }, StringSplitOptions.RemoveEmptyEntries);
                string paramKey = this.RemoveParamsSlashes(split[0].Trim());
                string paramValue = this.RemoveParamsSlashes(split[1].Trim());

                this.Parameters.Add(paramKey, paramValue);
            }

            Console.WriteLine("Name: {0}", this.Name);
            foreach (var parameter in this.Parameters)
            {
                Console.WriteLine("Key: {0}, Value: {1}", parameter.Key, parameter.Value);
            }
        }

        private string RemoveParamsSlashes(string str)
        {
            if (str.IndexOf('"') != -1)
            {
                int stringLength = str.Length;
                string output = str.Substring(1, stringLength - 2);
                return output;
            }

            return str;
        }
    }
}
