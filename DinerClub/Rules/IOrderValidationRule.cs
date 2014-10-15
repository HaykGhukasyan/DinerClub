using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents a general validation rule for all orders.
    /// </summary>
    public interface IOrderValidationRule
    {
        bool Validate(IOrder order, IEnumerable<IOrder> previousOrders, DayTime dayTime);
    }
}
