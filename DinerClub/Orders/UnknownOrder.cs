using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Represents an unknown order type.
    /// </summary>
    [RestrictDayTimeOrderValidationRuleAttribute(DayTime = DayTime.Morning)]
    [RestrictDayTimeOrderValidationRuleAttribute(DayTime = DayTime.Night)]
    public class UnknownOrder : IOrder
    {
        public string ToString(DayTime dayTime)
        {
            return string.Empty;
        }

        public int OrderType
        {
            get { return 0; }
        }
    }
}
