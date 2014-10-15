using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DinerClub.Tests
{
    [TestClass]
    public class CommandLineArgsTests
    {
        [ExpectedException(typeof(InvalidCommandLineArgumentException))]
        [TestMethod]
        public void Constructor_NullInputArguments_ThrowsArgumentNullException()
        {
            new CommandLineArgs(null);
        }

        [ExpectedException(typeof(InvalidCommandLineArgumentException))]
        [TestMethod]
        public void Constructor_EmptySetInputArguments_ThrowsInvalidCommandLineArgumentException()
        {
            new CommandLineArgs(new string[0]);
        }

        [ExpectedException(typeof(InvalidCommandLineArgumentException))]
        [TestMethod]
        public void Constructor_SingleInputArguments_ThrowsInvalidCommandLineArgumentException()
        {
            new CommandLineArgs(new string[1]);
        }

        [ExpectedException(typeof(InvalidCommandLineArgumentException))]
        [TestMethod]
        public void Constructor_NullDayInputArgument_ThrowsInvalidCommandLineArgumentException()
        {
            new CommandLineArgs(new string[]{ null, "1" });
        }

        [ExpectedException(typeof(InvalidCommandLineArgumentException))]
        [TestMethod]
        public void Constructor_EmptyDayInputArgument_ThrowsInvalidCommandLineArgumentException()
        {
            new CommandLineArgs(new string[] { string.Empty, "1" });
        }

        [TestMethod]
        public void Constructor_MorningAndOneOrder_ParsesArgumentsCorrectly()
        {
            var parameters = new string[] { "morning," , "1," };
            var target = new CommandLineArgs(parameters);

            Assert.AreEqual(DayTime.Morning, target.DayTime);
            Assert.IsNotNull(target.OrderNumbers, "Order numbers is null.");
            Assert.AreEqual(1, target.OrderNumbers.Count(), "OrderNumbers count does not match expected count");
            Assert.AreEqual(1, target.OrderNumbers.First(), "Order number does not match expected order number");
        }

        [TestMethod]
        public void Constructor_MorningAndTwoOrders_ParsesArgumentsCorrectly()
        {
            var parameters = new string[] { "morning," ,"1,", "1" };
            var target = new CommandLineArgs(parameters);

            Assert.AreEqual(DayTime.Morning, target.DayTime);
            Assert.AreEqual(2, target.OrderNumbers.Count(), "OrderNumbers count does not match expected count");
            Assert.AreEqual(1, target.OrderNumbers.ElementAt(0), "Order number does not match expected order number");
            Assert.AreEqual(1, target.OrderNumbers.ElementAt(1), "Order number does not match expected order number");
        }

        [TestMethod]
        public void Constructor_MorningAndThreeOrder_ParsesArgumentsCorrectly()
        {
            var parameters = new string[] { "morning,", "1,", "2,", "3" };
            var target = new CommandLineArgs(parameters);

            Assert.AreEqual(DayTime.Morning, target.DayTime);
            Assert.AreEqual(3, target.OrderNumbers.Count(), "OrderNumbers count does not match expected count");
            Assert.AreEqual(1, target.OrderNumbers.ElementAt(0), "Order number does not match expected order number");
            Assert.AreEqual(2, target.OrderNumbers.ElementAt(1), "Order number does not match expected order number");
            Assert.AreEqual(3, target.OrderNumbers.ElementAt(2), "Order number does not match expected order number");
        }

        [TestMethod]
        public void Constructor_NightArbitraryOrders_ParsesArgumentsCorrectly()
        {
            var parameters = new string[] { "morning,", "1,", "2,", "3,", "3,", "5,", "5" };
            var target = new CommandLineArgs(parameters);

            Assert.AreEqual(DayTime.Morning, target.DayTime);
            Assert.AreEqual(6, target.OrderNumbers.Count(), "OrderNumbers count does not match expected count");
            Assert.AreEqual(1, target.OrderNumbers.ElementAt(0), "Order number does not match expected order number");
            Assert.AreEqual(2, target.OrderNumbers.ElementAt(1), "Order number does not match expected order number");
            Assert.AreEqual(3, target.OrderNumbers.ElementAt(2), "Order number does not match expected order number");
            Assert.AreEqual(3, target.OrderNumbers.ElementAt(3), "Order number does not match expected order number");
            Assert.AreEqual(5, target.OrderNumbers.ElementAt(4), "Order number does not match expected order number");
            Assert.AreEqual(5, target.OrderNumbers.ElementAt(5), "Order number does not match expected order number");
        }
    }
}
