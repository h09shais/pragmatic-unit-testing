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
            // decide birthday discount
            var birthdayDiscountPercentage = Calculate.DiscountForMemberBirthday(when, findMember());

            // decide promo discount
            var promoDiscountPercentage = Calculate.DiscountForPromoCode(promoCode, when);

            var discountToApply = Math.Max(birthdayDiscountPercentage, promoDiscountPercentage);

            // decide applicable discount and create purchases
            var totalPayable = Calculate.TotalPayable(discountToApply, findItems());

            chargeMember(totalPayable);
        }
    }
}
