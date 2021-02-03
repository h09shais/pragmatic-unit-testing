using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Core.Models;
using Shopping.Core.Repositories;

namespace Shopping.Core.Services
{
    public class ShoppingService
    {
        public static void Checkout(IEnumerable<int> itemIDs, int memberId, string promoCode, DateTime when)
        {
            var member = MemberRepository.FindById(memberId);
            if (member == null)
            {
                throw new MemberNotFoundException(memberId);
            }

            var items = ItemRepository.FindByIDs(itemIDs) ?? new Item[] { };

            // decide birthday discount
            var birthdayDiscountPercentage = Calculate.DiscountForMemberBirthday(when, member.Birthday);

            // decide promo discount
            var promoDiscountPercentage = Calculate.DiscountForPromoCode(promoCode, when);

            // decide applicable discount and create purchases
            var totalPayable = Calculate.TotalPayable(birthdayDiscountPercentage, promoDiscountPercentage, items);

            // log and persist
            LoggingService.Log(LogLevel.Info, $"We got member {member.Name} hooked!");

            PaymentService.Charge(memberId, totalPayable);
        }
    }
}
