using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DinerClub;
using System.Collections.Generic;
using DinerClub.Converters;

namespace DinerClub.IntegrationTests
{
    [TestClass]
    public class DinerClubApplicationIntegrationTests
    {
        private IConverter<int, IOrder> converter;
        private IOrderValidator orderValidator;
        private IOrderNameService orderNameService;

        [TestInitialize]
        public void Init()
        {
            this.orderNameService = new OrderNameService();
            this.converter = new OrderNumberToOrderConverter(this.orderNameService);
            this.orderValidator = new OrderValidator(DayTime.Morning);
        }

        [TestMethod]
        public void PositiveMorningTest_CorrectCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "morning", "1", "2", "3" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Eggs, Toast, Coffee";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void PositiveMorningTest_UnorderedDishesCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "morning", "2", "1", "3" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Eggs, Toast, Coffee";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void PositiveNightTest_CommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "night", "1", "2", "3", "4" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);
            this.orderValidator = new OrderValidator(DayTime.Night);
            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Steak, Potato, Wine, Cake";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void NoDesertAllowedTest_MorningDesertCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "morning", "1", "2", "3", "4" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Eggs, Toast, Coffee, error";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void MultipleDrinkAllowedTest_MultipleCoffeeCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "morning,", "1", "2", "3", "3", "3" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Eggs, Toast, Coffee(x3)";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void MultipleSideAllowedTest_MultipleSideCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "night", "1", "2", "2", "4" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);
            this.orderValidator = new OrderValidator(DayTime.Night);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Steak, Potato(x2), Cake";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void UnknownDishTypeTest_UnknownDishTypeInCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "night", "1", "2", "3", "5" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);
            this.orderValidator = new OrderValidator(DayTime.Night);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Steak, Potato, Wine, error";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }

        [TestMethod]
        public void UnknownDishAndNotAllowedMultipleDishTypesRequestedTest_UnknownDishTypeAndNotAllowedMultipleDishesInCommandArgs_ApplicationReturnsExpectedResult()
        {
            string[] commandParams = { "night", "1", "1", "2", "3", "5" };
            CommandLineArgs commandLineArgs = new CommandLineArgs(commandParams);
            this.orderValidator = new OrderValidator(DayTime.Night);

            var target = new DinerClubApplication(commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Steak, error";
            string actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult, "Output result is not equal to expected one.");
        }
    }
}
