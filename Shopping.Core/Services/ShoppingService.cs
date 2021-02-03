using System;
using System.Collections.Generic;
using Shopping.Core.Models;

namespace Shopping.Core.Services
{
    public class ShoppingService
    {
        public static void Checkout(
            IEnumerable<int> itemIds, 
            int memberId,
            string promoCode,
            DateTime when,
            Func<int, Member> findMemberById,
            Func<IEnumerable<int>, IEnumerable<Item>> findItemsByIds,
            Action<LogLevel, string> log,
            Action<int, decimal> chargeMember)
        {
            var member = findMemberById(memberId);
            if (member == null)
            {
                throw new MemberNotFoundException(memberId);
            }

            var items = findItemsByIds(itemIds);

            // decide birthday discount
            var birthdayDiscountPercentage = Calculate.DiscountForMemberBirthday(when, member.Birthday);

            // decide promo discount
            var promoDiscountPercentage = Calculate.DiscountForPromoCode(promoCode, when);

            // decide applicable discount and create purchases
            var totalPayable = Calculate.TotalPayable(birthdayDiscountPercentage, promoDiscountPercentage, items);

            // log and persist
            log(LogLevel.Info, $"We got member {member.Name} hooked!");

            chargeMember(memberId, totalPayable);
        }
    }
}
