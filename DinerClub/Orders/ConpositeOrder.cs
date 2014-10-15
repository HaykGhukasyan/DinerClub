using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerClub.Orders
{
    /// <summary>
    /// Represents a group of orders of same type.
    /// </summary>
    public class CompositeOrder : IOrder
    {
        private IEnumerable<IOrder> orders;
        public CompositeOrder(IEnumerable<IOrder> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException("orders");
            }

            if (orders.Count() == 0)
            {
                throw new ArgumentException("Orders can not be an empty set.", "orders");
            }

            this.orders = orders;
        }
        
        /// <summary>
        /// Returns multiple orders string representation or first one's in case single order.
        /// </summary>
        /// <param name="dayTime">Day time.</param>
        /// <returns>Formatted string for multiple items.</returns>
        public string ToString(DayTime dayTime)
        {
            var count = orders.Count();

            if (count == 1)
            {
                return orders.First().ToString(dayTime);
            }

            return string.Format("{0}(x{1})", orders.First().ToString(dayTime), count);
        }

        public int OrderType
        {
            get { return this.orders.First().OrderType; }
        }
    }
}
