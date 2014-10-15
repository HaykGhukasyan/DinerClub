using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Rule for single order.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SingleOrderValidationRuleAttribute : OrderValidationRuleAttribute
    {
        private object uniqueTypeId = new object();

        public override bool Validate(IOrder order, IEnumerable<IOrder> previousOrders, DayTime dayTime)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            if (previousOrders == null)
            {
                throw new ArgumentNullException("previousOrders");
            }

            return this.DayTime == dayTime && !previousOrders.Any(o => o.OrderType == order.OrderType);
        }

        public override object TypeId
        {
            get
            {
                return this.uniqueTypeId;
            }
        }
    }
}
