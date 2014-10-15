using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Restrincting rule.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RestrictDayTimeOrderValidationRuleAttribute : OrderValidationRuleAttribute
    {
        private object uniqueTypeId = new object();
        public override bool Validate(IOrder order, IEnumerable<IOrder> previousOrders, DayTime dayTime)
        {
            return dayTime != this.DayTime;
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
