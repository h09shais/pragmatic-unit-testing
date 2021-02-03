using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class NewCalculateDiscountTests
    {
        public static object[] CalculatePromoDiscountTestCaseSource = {
            new object[]
            {
                "AM",
                new DateTime(2020, 10, 10, 10, 10, 10),
                8m
            },
            new object[]
            {
                "AM",
                new DateTime(2020, 10, 10, 22, 10, 10),
                0m
            },
            new object[]
            {
                "PM",
                new DateTime(2020, 10, 10, 22, 10, 10),
                6m
            },
            new object[]
            {
                "PM",
                new DateTime(2020, 10, 10, 10, 10, 10),
                0m
            },
        };

        [Theory]
        [TestCaseSource(nameof(CalculatePromoDiscountTestCaseSource))]
        public void CalculatePromoDiscountTests(string promoCode, DateTime when, decimal expected)
        {
            var calculatePromoDiscount = Calculate.DiscountForPromoCode(promoCode, when);
            Assert.AreEqual(expected, calculatePromoDiscount);
        }


        public static object[] CalculateDiscountForMemberBirthdayTestCaseSource = {
            new object[]
            {
                new DateTime(1986,08,06),
                new DateTime(2020, 08, 06, 10, 10, 10),
                50m
            },
            new object[]
            {
                new DateTime(1986,08,07),
                new DateTime(2020, 08, 06, 10, 10, 10),
                0m
            },
        };

        [Theory]
        [TestCaseSource(nameof(CalculateDiscountForMemberBirthdayTestCaseSource))]
        public void CalculateDiscountForMemberBirthdayTests(DateTime birthday, DateTime when, decimal expected)
        {
            var calculatePromoDiscount = Calculate.DiscountForMemberBirthday(when, birthday);
            Assert.AreEqual(expected, calculatePromoDiscount);
        }

        public static object[] CalculateTotalPayableTestCaseSource = {
            new object[]
            {
                new[]
                {
                    new Item() { IsDiscountable = true, Name = "bla", Price = 50m},
                },
                50m,
                10m,
                25m
            },
            new object[]
            {
                new[]
                {
                    new Item() { IsDiscountable = false, Name = "bla", Price = 50m},
                },
                50m,
                20m,
                50m
            },
            new object[]
            {
                new[]
                {
                    new Item() { IsDiscountable = true, Name = "bla", Price = 50m},
                    new Item() { IsDiscountable = false, Name = "bla", Price = 50m},
                },
                50m,
                30m,
                75m
            },
        };

        [Theory]
        [TestCaseSource(nameof(CalculateTotalPayableTestCaseSource))]
        public void CalculateTotalPayableTests(Item[] items, decimal birthdayDiscountPercentage, decimal promoDiscountPercentage, decimal expected)
        {
            var discountToApply = Math.Max(birthdayDiscountPercentage, promoDiscountPercentage);
            var totalPayable = items.Sum(item =>
            {
                if (item.IsDiscountable)
                {
                    return item.Price * (100 - discountToApply) / 100;
                }

                return item.Price;
            });
            var calculatePromoDiscount = Calculate.TotalPayable(birthdayDiscountPercentage, promoDiscountPercentage, items);
            Assert.AreEqual(expected, calculatePromoDiscount);
        }

        public static object[][] TotalChargedTestCases = {
            // buying nothing
            new object[]
            {
                new Item[] {},
                new Member
                {
                    Birthday = new DateTime(1983, 4, 2)
                },
                null,
                new DateTime(2019, 4, 1),
                0
            },
            // having birthday!
            new object[]
            {
                new[] { new Item
                {
                    Price = 100, 
                    IsDiscountable = true
                }},
                new Member
                {
                    Birthday = new DateTime(1983, 4, 2)
                },
                null,
                new DateTime(2019, 4, 2),
                50
            },
        };

        [Theory]
        [TestCaseSource(nameof(TotalChargedTestCases))]
        public void Member_is_charged_per_discounts(
            IEnumerable<int> itemIds, 
            IEnumerable<Item> items,
            int memberId, 
            Member member, 
            string promoCode, 
            DateTime checkoutTime,
            decimal expectedCharged)
        {
            var chargeMemberMock = new Mock<Action<int, decimal>>();
            ShoppingService.Checkout(
                itemIds, 
                memberId, 
                promoCode, 
                checkoutTime, 
                id => member,
                ids => items,
                (level, msg) => { },
                chargeMemberMock.Object);

            chargeMemberMock.Verify(item => item(memberId, expectedCharged), Times.Once);
        }
    }
}
