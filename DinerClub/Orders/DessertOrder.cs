using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents a desert order.
    /// </summary>
    [RestrictDayTimeOrderValidationRuleAttribute(DayTime = DayTime.Morning)]
    [SingleOrderValidationRule(DayTime = DayTime.Night)]
    public class DessertOrder : Order
    {
        public DessertOrder(int orderType, IOrderNameService orderNameService) :
            base(orderType, orderNameService)
        {

        }
    }
}
