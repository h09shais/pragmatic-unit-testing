using System;
using System.Collections.Generic;
using Shopping.Core.Models;

namespace Shopping.Core.Services
{
    public class ShoppingService
    {
        public static void Checkout(
            string promoCode,
            DateTime when,
            Func<DateTime> findMember,
            Func<IEnumerable<Item>> findItems,
            Action<decimal> chargeMember)
        {
            // decide birthday discount
            var birthdayDiscountPercentage = Calculate.DiscountForMemberBirthday(when, findMember());

            // decide promo discount
            var promoDiscountPercentage = Calculate.DiscountForPromoCode(promoCode, when);

            // decide applicable discount and create purchases
            var totalPayable = Calculate.TotalPayable(birthdayDiscountPercentage, promoDiscountPercentage, findItems());

            // log and persist
            //log(LogLevel.Info, $"We got member {member.Name} hooked!");

            chargeMember(totalPayable);
        }
    }
}
