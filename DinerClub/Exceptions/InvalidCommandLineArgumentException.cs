using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents a general exception for invalid command line arguments.
    /// </summary>
    public class InvalidCommandLineArgumentException : Exception
    {
        private const string DefaultErrorMessage = "Invalid command line arguments are entered.";

        private const string UsageExamples = "morning, 1, 2, 3\r\n" + "morning, 2, 1, 3\r\n" + "morning, 1, 2, 3, 3, 3\r\n";

        /// <summary>
        /// Instanciates instance of <see cref="InvalidCommandLineArgumentException"/> class.
        /// </summary>
        public InvalidCommandLineArgumentException() : base(DefaultErrorMessage)
        {

        }
        
        /// <summary>
        /// Instanciates instance of <see cref="InvalidCommandLineArgumentException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidCommandLineArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Gets command arguments usage examples.
        /// </summary>
        public string Examples
        {
            get { return UsageExamples; }
        }
    }
}
