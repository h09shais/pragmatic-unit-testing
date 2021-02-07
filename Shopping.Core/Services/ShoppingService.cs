using System;
using System.Collections.Generic;
using Shopping.Core.Models;
using Shopping.Core.Providers;

namespace Shopping.Core.Services
{
    public class ShoppingService
    {
        public static void Checkout(
            string promoCode,
            DateTime when,
            Func<Member> findMember,
            Func<IEnumerable<Item>> findItems,
            Action<decimal> chargeMember)
        {
            var birthdayDiscountPercentage = Calculate.DiscountForMemberBirthday(when, findMember());

            var promoDiscountPercentage = Calculate.DiscountForPromoCode(promoCode, when);

            var discountToApply = Math.Max(birthdayDiscountPercentage, promoDiscountPercentage);

            var totalPayable = Calculate.TotalPayable(discountToApply, findItems());

            chargeMember(totalPayable);
        }
    }
}
