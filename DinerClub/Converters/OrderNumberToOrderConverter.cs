using DinerClub.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinerClub
{
    /// <summary>
    /// Converters order numbers to appropriate orders.
    /// </summary>
    public class OrderNumberToOrderConverter : IConverter<int, IOrder>
    {
        private IDictionary<int, IDictionary<DayTime, string>> knownOrders;
        private IOrderNameService orderNameService;

        public OrderNumberToOrderConverter(IOrderNameService orderNameService)
        {
            if(orderNameService == null)
            {
                throw new ArgumentNullException("orderNameService");
            }

            this.orderNameService = orderNameService;
        }

        /// <summary>
        /// Converts command line arguments to appropriate orders.
        /// </summary>
        /// <param name="input">Order number.</param>
        /// <returns>Appropriate order.</returns>
        public IOrder Convert(int input)
        {
            switch(input)
            {
                case 1:
                    return new EntréeOrder(input, orderNameService);
                case 2:
                    return new SideOrder(input, orderNameService);
                case 3:
                    return new DrinkOrder(input, orderNameService);
                case 4:
                    return new DessertOrder(input, orderNameService);
                default:
                    return new UnknownOrder();
            }
        }
    }
}
