using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represetns an entrée order.
    /// </summary>
    [SingleOrderValidationRule(DayTime = DayTime.Morning)]
    [SingleOrderValidationRule(DayTime = DayTime.Night)]
    public class EntréeOrder : Order
    {
        public EntréeOrder(int orderType, IOrderNameService orderNameService) 
            : base(orderType, orderNameService)
        {

        }
    }
}
