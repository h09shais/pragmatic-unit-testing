using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Core.Models;

namespace Shopping.Core.Services
{
    public class Calculate
    {
        public static decimal TotalPayable(decimal birthdayDiscountPercentage, decimal promoDiscountPercentage,
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

        public static decimal DiscountForMemberBirthday(DateTime when, DateTime birthday)
        {
            var birthdayDiscountPercentage = 0;
            var isBirthday = birthday.Month == when.Month && birthday.Day == when.Day;
            if (isBirthday)
            {
                birthdayDiscountPercentage = 50;
            }

            return birthdayDiscountPercentage;
        }

        public static decimal DiscountForPromoCode(string promoCode, DateTime when)
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
