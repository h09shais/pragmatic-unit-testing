using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class ShoppingServiceIntegrationTests
    {
        public static object[][] TotalChargedTestCases = {
            // buying nothing
            new object[]
            {
                new Item[] {},
                new Member { Birthday = new DateTime(1983, 4, 2) },
                null,
                new DateTime(2019, 4, 1),
                0m
            },
            // having birthday!
            new object[]
            {
                new[] { new Item { Price = 100, IsDiscountable = true }},
                new Member { Birthday = new DateTime(1983, 4, 2) },
                null,
                new DateTime(2019, 4, 2),
                50m
            }
        };

        [Theory]
        [TestCaseSource(nameof(TotalChargedTestCases))]
        public void Member_is_charged_per_discounts(
            IEnumerable<Item> items,
            Member member,
            string promoCode,
            DateTime checkoutTime,
            decimal expectedTotalCharged)
        {
            // Arrange
            var chargeMemberMock = new Mock<Action<decimal>>();

            // Act
            ShoppingService.Checkout(
                promoCode,
                checkoutTime,
                () => member,
                () => items,
                chargeMemberMock.Object);

            // Assert
            chargeMemberMock.Verify(item => item(expectedTotalCharged), Times.Once);
        }
    }
}
