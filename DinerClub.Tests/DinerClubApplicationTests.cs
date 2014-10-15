using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DinerClub;
using DinerClub.Converters;

using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace DinerClub.Tests
{
    [TestClass]
    public class DinerClubApplicationTests
    {
        private IConverter<int, IOrder> converter;
        private ICommandLineArgs commandLineArgs;
        private IOrderValidator orderValidator;

        [TestInitialize]
        public void Init()
        {
            this.commandLineArgs = MockRepository.GenerateMock<ICommandLineArgs>();
            this.converter = MockRepository.GenerateStub<IConverter<int, IOrder>>();
            this.orderValidator = MockRepository.GenerateMock<IOrderValidator>();
        }

        [ExpectedException(typeof(ArgumentNullException), "CommandLineArgs is null.")]
        [TestMethod]
        public void Constructor_NullCommandLineArgs_ThrowsArgumentNullException()
        {
            new DinerClubApplication(
                null,
                this.converter,
                this.orderValidator);
        }

        [ExpectedException(typeof(ArgumentNullException), "Converter is null.")]
        [TestMethod]
        public void Constructor_NullCommandLineArgsConverter_ThrowsArgumentNullException()
        {
            new DinerClubApplication(this.commandLineArgs, null, this.orderValidator);
        }

        [ExpectedException(typeof(ArgumentNullException), "OrderValidator is null.")]
        [TestMethod]
        public void Constructor_NullOrderValidator_ThrowsArgumentNullException()
        {
            new DinerClubApplication(this.commandLineArgs, this.converter, null);
        }

        [TestMethod]
        public void Run_MockedCommandLineToOrdersConverter_CallsConvertMethod()
        {
            var commandLineArgs = MockRepository.GenerateStub<ICommandLineArgs>();
            commandLineArgs.Expect(x => x.DayTime).Return(DayTime.Morning);
            commandLineArgs.Expect(x => x.OrderNumbers).Return(new int[] { 1, 2, 3, 4 });

            this.converter.Expect(x => x.Convert(Arg<int>.Is.Anything)).Repeat.Equals(4);
            this.orderValidator.Expect(x => x.IsValid(Arg<IOrder>.Is.Anything, Arg<IEnumerable<IOrder>>.Is.Anything)).Return(true);

            var target = new DinerClubApplication(commandLineArgs, converter, this.orderValidator);

            target.Run();
            this.converter.VerifyAllExpectations();
        }

        [TestMethod]
        public void Run_MockedCommandLineToOrdersConverter_RetursExpectedResult()
        {
            this.commandLineArgs.Expect(x => x.DayTime).Return(DayTime.Morning);
            this.commandLineArgs.Expect(x => x.OrderNumbers).Return(new int[] { 1, 2, 3, 4 });

            var entréeOrder = MockRepository.GenerateStub<IOrder>();
            var sideOrder = MockRepository.GenerateStub<IOrder>();
            var drinkOrder = MockRepository.GenerateStub<IOrder>();
            var desertOrder = MockRepository.GenerateStub<IOrder>();

            entréeOrder.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("order1");
            entréeOrder.Expect(x => x.OrderType).Return(1);
            sideOrder.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("order2");
            sideOrder.Expect(x => x.OrderType).Return(2);
            drinkOrder.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("order3");
            drinkOrder.Expect(x => x.OrderType).Return(3);
            desertOrder.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("order4");
            desertOrder.Expect(x => x.OrderType).Return(4);

            var orders = new IOrder[]
            {
                entréeOrder, sideOrder, drinkOrder, desertOrder 
            };

            var previousOrders = Enumerable.Empty<IOrder>();
            this.converter.Expect(x => x.Convert(1)).Return(entréeOrder);
            this.converter.Expect(x => x.Convert(2)).Return(sideOrder);
            this.converter.Expect(x => x.Convert(3)).Return(drinkOrder);
            this.converter.Expect(x => x.Convert(4)).Return(desertOrder);

            this.orderValidator.Expect(x => x.IsValid(Arg<IOrder>.Is.Equal(entréeOrder), Arg<IEnumerable<IOrder>>.Is.Anything)).Return(true);
            this.orderValidator.Expect(x => x.IsValid(Arg<IOrder>.Is.Equal(sideOrder), Arg<IEnumerable<IOrder>>.Is.Anything)).Return(true);
            this.orderValidator.Expect(x => x.IsValid(Arg<IOrder>.Is.Equal(drinkOrder), Arg<IEnumerable<IOrder>>.Is.Anything)).Return(true);
            this.orderValidator.Expect(x => x.IsValid(Arg<IOrder>.Is.Equal(desertOrder), Arg<IEnumerable<IOrder>>.Is.Anything)).Return(false);
            
            var target = new DinerClubApplication(this.commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "order1, order2, order3, error";
            var actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Run_MockedCommandLineToOrdersConverterInvalidEntreeOrder_RetursExpectedResult()
        {
            this.commandLineArgs.Expect(x => x.DayTime).Return(DayTime.Morning);
            this.commandLineArgs.Expect(x => x.OrderNumbers).Return(new int[]{ 1, 1, 2, 3, 4 });

            var entréeOrder = MockRepository.GenerateStub<IOrder>();

            entréeOrder.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("order1");

            var previousOrders = Enumerable.Empty<IOrder>();
            
            this.converter.Expect(x => x.Convert(1)).Return(entréeOrder);

            var repository = this.orderValidator.GetMockRepository();

            using (repository.Ordered())
            {
                this.orderValidator.Expect(x => x.IsValid(entréeOrder, previousOrders)).Return(true);
                this.orderValidator.Expect(x => x.IsValid(entréeOrder, previousOrders)).Return(false);
            }

            var expectedResult = "order1, error";
            var target = new DinerClubApplication(this.commandLineArgs, this.converter, this.orderValidator);

            var actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Run_MockedCommandLineToOrdersConverterRepeatedOrders_RetursExpectedResult()
        {
            this.commandLineArgs.Expect(x => x.OrderNumbers).Return(new int[] { 1, 1, 2, 2, 2, 3 });

            var order1 = MockRepository.GenerateMock<IOrder>();
            var order2 = MockRepository.GenerateMock<IOrder>();
            var order3 = MockRepository.GenerateMock<IOrder>();

            order1.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("Order1");
            order1.Expect(x => x.OrderType).Return(1);
            order2.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("Order2");
            order2.Expect(x => x.OrderType).Return(2);
            order3.Expect(x => x.ToString(Arg<DayTime>.Is.Anything)).Return("Order3");
            order3.Expect(x => x.OrderType).Return(3);


            this.converter.Expect(x => x.Convert(1)).Return(order1);
            this.converter.Expect(x => x.Convert(2)).Return(order2);
            this.converter.Expect(x => x.Convert(3)).Return(order3);

            this.orderValidator.Expect(x => x.IsValid(Arg<IOrder>.Is.Anything, Arg<IEnumerable<IOrder>>.Is.Anything))
                .Return(true);
            var target = new DinerClubApplication(this.commandLineArgs, this.converter, this.orderValidator);

            var expectedResult = "Order1(x2), Order2(x3), Order3";
            var actualResult = target.Run();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
