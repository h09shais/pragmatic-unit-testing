using System;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class ShoppingServiceTests
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
            },
        };

        public static object[][] CalculateTotalPayableTestCases = {
            // no item at all
            new object[]
            {
                new Item[]{},
                20m,
                0m
            },
            // single discountable
            new object[]
            {
                new Item[]{ new Item { IsDiscountable = true, Price = 100 } },
                20m,
                80m
            },
            // add more test cases?
        };

        [Theory]
        [TestCaseSource(nameof(CalculateTotalPayableTestCases))]
        public void Calculate_total_payable(Item[] items, decimal discountToApply, decimal expectedTotal)
        {
            var actual = Calculate.TotalPayable(discountToApply, items);
            Assert.AreEqual(expectedTotal, actual);
        }

        [Test]
        public void If_member_is_buying_nothing_Then_should_not_be_charged()
        {
        }

        [Test]
        public void If_item_is_not_discountable_Although_member_is_having_birthday_Then_should_be_charged_fully()
        {
        }

        [Test]
        public void If_item_is_discountable_And_member_is_having_birthday_Then_discount_by_50_percent()
        {
        }

        [Test]
        public void If_item_is_discountable_And_member_is_having_birthday_And_promo_code_is_AM_and_time_is_AM_Then_discount_by_50_percent()
        {
            // try write this test
        }

        [Test]
        public void If_item_is_discountable_And_member_is_not_having_birthday_And_promo_code_is_AM_and_time_is_AM_Then_discount_by_8_percent()
        {
            // try write this test
        }
    }

}
