using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.ComponentModel;
using System.Collections.Generic;

namespace DinerClub.Tests
{
    [TestClass]
    public class OrderValidatorTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void IsValid_NullOrder_ThrowsArgumentNullException()
        {
            var target = new OrderValidator(DayTime.Morning);
            target.IsValid(null, new IOrder[0]);
        }

        [TestMethod]
        public void IsValid_OrderWithValidationAttribute_CallsValidateOnValidationAttribute()
        {
            var dayTime = DayTime.Morning;
            var order = MockRepository.GenerateStub<IOrder>();
            var orderValidationRule = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            TypeDescriptor.AddAttributes(order, orderValidationRule);
            var previousOrders = new IOrder[0];

            var target = new OrderValidator(dayTime);
            target.IsValid(order, previousOrders);

            orderValidationRule.AssertWasCalled(x => x.Validate(order, previousOrders, dayTime));
        }

        [TestMethod]
        public void IsValid_OrderWithMultipleValidationAttribute_ReturnsTrue()
        {
            var dayTime = DayTime.Morning;
            var previousOrders = new IOrder[0];
            var order = MockRepository.GenerateStub<IOrder>();
            var orderValidationRule1 = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            var orderValidationRule2 = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            orderValidationRule1.Expect(x => x.Validate(order, previousOrders, dayTime)).Return(true);
            orderValidationRule2.Expect(x => x.Validate(order, previousOrders, dayTime)).Return(true);
            TypeDescriptor.AddAttributes(order, orderValidationRule1, orderValidationRule2);

            var target = new OrderValidator(dayTime);
            Assert.IsTrue(target.IsValid(order, previousOrders));
        }

        [TestMethod]
        public void IsValid_OrderWithMultipleValidationAttribute_ReturnsFalse()
        {
            var dayTime = DayTime.Morning;
            var previousOrders = new IOrder[0];
            var order = MockRepository.GenerateStub<IOrder>();
            var orderValidationRule1 = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            var orderValidationRule2 = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            var negativeValidationRule = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            orderValidationRule1.Expect(x => x.Validate(order, previousOrders, dayTime)).Return(true);
            orderValidationRule2.Expect(x => x.Validate(order, previousOrders, dayTime)).Return(true);
            negativeValidationRule.Expect(x => x.Validate(order, previousOrders, dayTime)).Return(false);

            TypeDescriptor.AddAttributes(order, orderValidationRule1, orderValidationRule2, negativeValidationRule);

            var target = new OrderValidator(dayTime);
            Assert.IsFalse(target.IsValid(order, previousOrders));
        }

        [TestMethod]
        public void IsValid_OrderWithMultipleValidationAttribute_DoesNotCallNightAttributes()
        {
            var previousOrders = new IOrder[0];
            var order = MockRepository.GenerateStub<IOrder>();
            var morningValidationRule1 = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            morningValidationRule1.Expect(x => x.Validate(order, previousOrders, DayTime.Morning)).Return(true);
            var nightValidationRule = MockRepository.GenerateMock<OrderValidationRuleAttribute>();
            nightValidationRule.Expect(x => x.DayTime).Return(DayTime.Night);

            TypeDescriptor.AddAttributes(order, morningValidationRule1, nightValidationRule);

            var target = new OrderValidator(DayTime.Morning);

            target.IsValid(order, previousOrders);

            morningValidationRule1.AssertWasCalled(x => x.Validate(order, previousOrders, DayTime.Morning));
            nightValidationRule.AssertWasNotCalled(x => x.Validate(
                Arg<IOrder>.Is.Anything, 
                Arg<IEnumerable<IOrder>>.Is.Anything, 
                Arg<DayTime>.Is.Anything));
        }
    }
}
