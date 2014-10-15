using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Interface for order name service.
    /// </summary>
    public interface IOrderNameService
    {
        /// <summary>
        /// Returns name of the order by day time.
        /// </summary>
        /// <param name="dayTime">Day time.</param>
        /// <returns>Name of the order.</returns>
        string GetOrderName(IOrder order, DayTime dayTime);
    }
}
