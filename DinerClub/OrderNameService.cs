using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerClub
{
    /// <summary>
    /// Service for retrieving order name in respect of day time. 
    /// </summary>
    public class OrderNameService : IOrderNameService
    {
        private IDictionary<int, IDictionary<DayTime, string>> knownOrders;
        public OrderNameService()
        {
            knownOrders = new Dictionary<int, IDictionary<DayTime, string>>();
            knownOrders[1] = new Dictionary<DayTime, string>()
                {
                    { DayTime.Morning, "Eggs"},
                    { DayTime.Night, "Steak"}
                };
            knownOrders[2] = new Dictionary<DayTime, string>()
                {
                    { DayTime.Morning, "Toast"},
                    { DayTime.Night, "Potato"}
                };
            knownOrders[3] = new Dictionary<DayTime, string>()
                {
                    { DayTime.Morning, "Coffee"},
                    { DayTime.Night, "Wine"}
                };
            knownOrders[4] = new Dictionary<DayTime, string>()
                {
                    { DayTime.Morning, string.Empty},
                    { DayTime.Night, "Cake"}
                };
        }
        
        /// <summary>
        /// Return order name for given day time.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="dayTime">The day time.</param>
        /// <returns>String as order name if available, otherwise an empty string.</returns>
        public string GetOrderName(IOrder order, DayTime dayTime)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }

            if (this.knownOrders.ContainsKey(order.OrderType) 
                && this.knownOrders[order.OrderType] != null
                && this.knownOrders[order.OrderType].ContainsKey(dayTime))
            {
                return this.knownOrders[order.OrderType][dayTime];
            }

            return string.Empty;
        }
    }
}
