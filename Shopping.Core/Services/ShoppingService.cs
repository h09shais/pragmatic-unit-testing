using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Core.Models;
using Shopping.Core.Repositories;

namespace Shopping.Core.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly ILoggingService _loggingService;
        private readonly IItemRepository _itemRepo;
        private readonly IPaymentService _paymentService;
        private readonly IMemberRepository _memberRepo;

        public ShoppingService(ILoggingService loggingService,
            IItemRepository itemRepo,
            IPaymentService paymentService,
            IMemberRepository memberRepo)
        {
            _loggingService = loggingService;
            _itemRepo = itemRepo;
            _paymentService = paymentService;
            _memberRepo = memberRepo;
        }

        public void Checkout(IEnumerable<int> itemIDs, int memberId, string promoCode, DateTime when)
        {
            var member = _memberRepo.FindById(memberId);
            if (member == null)
            {
                throw new MemberNotFoundException(memberId);
            }

            var items = _itemRepo.FindByIDs(itemIDs) ?? new Item[] { };

            // decide birthday discount
            var birthdayDiscountPercentage = CalculateDiscountForMemberBirthday(when, member.Birthday);

            // decide promo discount
            var promoDiscountPercentage = CalculateDiscountForPromoCode(promoCode, when);

            // decide applicable discount and create purchases
            var totalPayable = CalculateTotalPayable(birthdayDiscountPercentage, promoDiscountPercentage, items);

            // log and persist
            _loggingService.Log(LogLevel.Info, $"We got member {member.Name} hooked!");

            _paymentService.Charge(memberId, totalPayable);
        }

        public static decimal CalculateTotalPayable(decimal birthdayDiscountPercentage, decimal promoDiscountPercentage,
            IEnumerable<Item> items)
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

            return totalPayable;
        }

        public static decimal CalculateDiscountForMemberBirthday(DateTime when, DateTime birthday)
        {
            var birthdayDiscountPercentage = 0;
            var isBirthday = birthday.Month == when.Month && birthday.Day == when.Day;
            if (isBirthday)
            {
                birthdayDiscountPercentage = 50;
            }

            return birthdayDiscountPercentage;
        }

        public static decimal CalculateDiscountForPromoCode(string promoCode, DateTime when)
        {
            var promoDiscountPercentage = 0;

            if (promoCode == "AM" && when.Hour < 12)
            {
                promoDiscountPercentage = 8;
            }
            else if (promoCode == "PM" && when.Hour >= 12)
            {
                promoDiscountPercentage = 6;
            }

            return promoDiscountPercentage;
        }
    }
}
