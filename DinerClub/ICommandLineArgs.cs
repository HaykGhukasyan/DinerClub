using System;
using System.Collections.Generic;
namespace DinerClub
{
    /// <summary>
    /// Interface for command line arguments.
    /// </summary>
    public interface ICommandLineArgs
    {
        /// <summary>
        /// Gets the day time.
        /// </summary>
        DayTime DayTime { get; }
        /// <summary>
        /// Gets the order numbers.
        /// </summary>
        IEnumerable<int> OrderNumbers { get; }
    }
}
