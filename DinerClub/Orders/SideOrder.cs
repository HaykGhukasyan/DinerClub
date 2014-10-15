using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents a side order.
    /// </summary>
    [MultipleOrderValidationRuleAttribute(DayTime = DayTime.Night)]
    [SingleOrderValidationRule(DayTime = DayTime.Morning)]
    public class SideOrder : Order
    {

        public SideOrder(int orderType, IOrderNameService orderNameService) : 
            base(orderType, orderNameService)
        {

        }
    }
}
