﻿using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class ShoppingServiceTests
    {
        [Test]
        public void If_member_is_buying_nothing_Then_should_not_be_charged()
        {
            // Arrange
            var itemIDs = Enumerable.Empty<int>();
            var memberID = 12345;
            var member = new Member { Birthday = DateTime.Now };

            var loggingServiceMock = new Mock<ILoggingService>();
            var paymentServiceMock = new Mock<IPaymentService>();

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(i => i.FindByIDs(itemIDs)).Returns(new Item[] { });

            var memberRepoMock = new Mock<IMemberRepository>();
            memberRepoMock.Setup(m => m.FindById(memberID)).Returns(member);

            var shoppingService = new ShoppingService(loggingServiceMock.Object,
                itemRepoMock.Object,
                paymentServiceMock.Object,
                memberRepoMock.Object);

            // Act
            shoppingService.Checkout(itemIDs, memberID, promoCode: null, when: DateTime.Now);

            // Assert
            paymentServiceMock.Verify(r => r.Charge(memberID, 0), Times.Once);
        }

        [Test]
        public void If_item_is_not_discountable_Although_member_is_having_birthday_Then_should_be_charged_fully()
        {
            // Arrange
            var itemIDs = new int[] { 11, 22 };
            var items = new Item[]
            {
                new Item { Name = "foo", Price = 50 },
                new Item { Name = "bar", Price = 100 },
            };
            var memberID = 12345;
            var member = new Member { Birthday = DateTime.Now };

            var loggingServiceMock = new Mock<ILoggingService>();
            var paymentServiceMock = new Mock<IPaymentService>();

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(i => i.FindByIDs(itemIDs)).Returns(items);

            var memberRepoMock = new Mock<IMemberRepository>();
            memberRepoMock.Setup(m => m.FindById(memberID)).Returns(member);

            var shoppingService = new ShoppingService(loggingServiceMock.Object,
                itemRepoMock.Object,
                paymentServiceMock.Object,
                memberRepoMock.Object);

            // Act
            shoppingService.Checkout(itemIDs, memberID, promoCode: null, when: DateTime.Now);

            // Assert
            paymentServiceMock.Verify(r => r.Charge(memberID, 150), Times.Once);
        }

        [Test]
        public void If_item_is_discountable_And_member_is_having_birthday_Then_discount_by_50_percent()
        {
            // Arrange
            var itemIDs = new int[] { 11, 22 };
            var items = new Item[]
            {
                new Item { Name = "foo", Price = 50 },
                new Item { Name = "bar", Price = 100 },
            };
            var memberId = 12345;
            var member = new Member { Birthday = DateTime.Now };

            var loggingServiceMock = new Mock<ILoggingService>();
            var paymentServiceMock = new Mock<IPaymentService>();

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(i => i.FindByIDs(itemIDs)).Returns(items);

            var memberRepoMock = new Mock<IMemberRepository>();
            memberRepoMock.Setup(m => m.FindById(memberId)).Returns(member);

            var shoppingService = new ShoppingService(loggingServiceMock.Object,
                itemRepoMock.Object,
                paymentServiceMock.Object,
                memberRepoMock.Object);

            // Act
            shoppingService.Checkout(itemIDs, memberId, promoCode: null, when: DateTime.Now);

            // Assert
            var discountToApply = 50;
            var totalPayable = items.Sum(item =>
            {
                if (item.IsDiscountable)
                {
                    return item.Price * (100 - discountToApply) / 100;
                }
                return item.Price;
            });

            paymentServiceMock.Verify(r => r.Charge(memberId, totalPayable), Times.Once);
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
