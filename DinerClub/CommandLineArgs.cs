using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents command line arguments as input.
    /// </summary>
    public class CommandLineArgs : ICommandLineArgs
    {
        private char[] Separator = new char[] {',', ' '};
        private string[] commandParams;

        /// <summary>
        /// Creates instance of CommandLineArgs. 
        /// </summary>
        /// <param name="commandParams">Command line parameters.</param>
        public CommandLineArgs(string[] commandParams)
        {
            if (commandParams == null || 
                commandParams.Length < 2)
            {
                throw new InvalidCommandLineArgumentException("At least Day and one Dish type should be requested.");
            }

            foreach (var param in commandParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    throw new InvalidCommandLineArgumentException("Parameter can not be empty.");
                }
            }

            this.commandParams = commandParams.Select(s => s.Trim(Separator)).ToArray();
            this.SetDayTime();
            this.OrderNumbers = this.commandParams.Skip(1).Select(int.Parse);
        }

        private void SetDayTime()
        {
            DayTime dayTime;
            if (!Enum.TryParse(this.commandParams[0], true, out dayTime))
            {
                throw new InvalidCommandLineArgumentException("First parameter should be Day.");
            }

            this.DayTime = dayTime;
        }
        public DayTime DayTime { get; set; }

        public IEnumerable<int> OrderNumbers { get; set; }
    }
}
