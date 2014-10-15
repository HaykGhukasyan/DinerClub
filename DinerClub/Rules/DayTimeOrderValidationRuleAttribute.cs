using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents the base rule class for all order validations.
    /// </summary>
    public abstract class OrderValidationRuleAttribute : Attribute, IOrderValidationRule
    {
        public virtual bool Validate(IOrder order, IEnumerable<IOrder> previousOrders, DayTime dayTime)
        {
            return true;
        }

        public virtual DayTime DayTime { get; set; }
    }
}
