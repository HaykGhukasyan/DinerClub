using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// General interface for all orders.
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// Returns order name. 
        /// </summary>
        /// <param name="dayTime">Day time.</param>
        /// <returns>Order's string representation.</returns>
        string ToString(DayTime dayTime);

        int OrderType { get; }
    }
}
