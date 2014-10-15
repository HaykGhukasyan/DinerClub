using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents a drink order.
    /// </summary>
    [SingleOrderValidationRuleAttribute(DayTime = DayTime.Night)]
    [MultipleOrderValidationRule(DayTime = DayTime.Morning)]
    public class DrinkOrder : Order
    {
        public DrinkOrder (int orderType, IOrderNameService orderNameService) : 
            base(orderType, orderNameService)
	    {

	    }
    }
}
