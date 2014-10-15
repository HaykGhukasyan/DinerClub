using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DinerClub.Converters;
using System.Collections.ObjectModel;
using DinerClub.Orders;

namespace DinerClub
{
    /// <summary>
    /// Manages dinary order requests and provides printed order request. 
    /// </summary>
    public class DinerClubApplication
    {
        private const string OrderError = "error";
        private const string Separator = ", ";

        private ICommandLineArgs commandLineArgs;
        private IConverter<int, IOrder> orderConverter;
        private IOrderValidator orderValidator;

        /// <summary>
        /// Creates an instance of <see cref="DinerClubApplication"/> class.
        /// </summary>
        /// <param name="commandParams">Command line arguments.</param>
        public DinerClubApplication(
            ICommandLineArgs commandLineArgs,
            IConverter<int, IOrder> orderConverter,
            IOrderValidator orderValidator)
        {
            if (commandLineArgs == null)
            {
                throw new ArgumentNullException("commandLineArgs");
            }

            if (orderConverter == null)
            {
                throw new ArgumentNullException("orderConverter");
            }

            if (orderValidator == null)
            {
                throw new ArgumentNullException("orderValidator");
            }

            this.commandLineArgs = commandLineArgs;
            this.orderConverter = orderConverter;
            this.orderValidator = orderValidator;
        }
        
        /// <summary>
        /// Processes given dish orders in command line arguments.
        /// </summary>
        /// <returns>A string representing accepted order.</returns>
        public string Run()
        {
            ICollection<IOrder> validOrders = new Collection<IOrder>();
            bool hasInvalidOrder = false;

            var orders = this.commandLineArgs.OrderNumbers.Select(this.orderConverter.Convert);
            
            foreach (var order in orders)
	        {
                if (!this.orderValidator.IsValid(order, validOrders))
	            {
                    hasInvalidOrder = true;
                    break;
	            }

                validOrders.Add(order);
            }

            var orderedGroupedValidOrders = validOrders
                .GroupBy(x => x.OrderType)
                .OrderBy(x => x.Key)
                .Select(x => new CompositeOrder(x));

            var validOrdersToPrint = string.Join(Separator, orderedGroupedValidOrders.Select(x => x.ToString(this.commandLineArgs.DayTime)));

            if (hasInvalidOrder)
            {
                validOrdersToPrint = string.Concat(validOrdersToPrint, Separator, OrderError);
            }
            
            return validOrdersToPrint;
        }
    }
}
