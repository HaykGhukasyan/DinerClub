using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents a validation manager for orders validation. 
    /// </summary>
    public class OrderValidator : IOrderValidator
    {
        private DayTime dayTime;
        /// <summary>
        /// Creates instance of OrderValidator.
        /// </summary>
        /// <param name="dayTime">Day time.</param>
        public OrderValidator (DayTime dayTime)
	    {
            this.dayTime = dayTime;
	    }
        /// <summary>
        /// Checks if order is valid for given day time in previous orders context.
        /// </summary>
        /// <param name="order">The order to check.</param>
        /// <param name="previousOrders">Set of previous validated orders.</param>
        /// <returns>True if valid, otherwise false.</returns>
        public bool IsValid(IOrder order, IEnumerable<IOrder> previousOrders)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            var validationRules = TypeDescriptor.GetAttributes(order)
                .OfType<OrderValidationRuleAttribute>()
                .Where(r => r.DayTime == this.dayTime);

            return validationRules.All(r => r.Validate(order, previousOrders, this.dayTime));
        }
    }
}
