using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Base order class.
    /// </summary>
    public abstract class Order : IOrder
    {
        protected IOrderNameService orderNameService;
        protected int orderType;
        protected Order(int orderType, IOrderNameService orderNameService)
        {
            this.orderType = orderType;
            this.orderNameService = orderNameService;
        }

        public virtual string ToString(DayTime dayTime)
        {
            return this.orderNameService.GetOrderName(this, dayTime);
        }

        public virtual int OrderType { get { return this.orderType; } }
    }
}
