using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Collections.ObjectModel;

namespace DinerClub.Tests
{
    [TestClass]
    public class OrderNumberToOrderConverterTests
    {
        private IOrderNameService orderNameService;

        [TestInitialize]
        public void Init()
        {
            this.orderNameService = MockRepository.GenerateMock<IOrderNameService>();
        }

        [TestMethod]
        public void Convert_UnknownOrderType_ReturnsUnknownOrder()
        {
            var result = new OrderNumberToOrderConverter(this.orderNameService).Convert(15);
            Assert.IsInstanceOfType(result, typeof(UnknownOrder));
        }

        [TestMethod]
        public void Convert_KnownOrderTypes_ReturnsExpectedOrders()
        {
            var orderNumbers = new int[] { 1, 2, 3, 4 };

            var target = new OrderNumberToOrderConverter(this.orderNameService);

            var results = orderNumbers.Select(o => target.Convert(o)).ToArray();

            Assert.IsInstanceOfType(results[0], typeof(EntréeOrder));
            Assert.IsInstanceOfType(results[1], typeof(SideOrder));
            Assert.IsInstanceOfType(results[2], typeof(DrinkOrder));
            Assert.IsInstanceOfType(results[3], typeof(DessertOrder));
        }
    }
}
