using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// General interface for orders validation manager.
    /// </summary>
    public interface IOrderValidator
    {
        bool IsValid(IOrder order, IEnumerable<IOrder> previousOrders);
    }
}
