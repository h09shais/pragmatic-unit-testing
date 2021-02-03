using System;
using System.Collections.Generic;

namespace Shopping.Core.Services
{
    public interface IShoppingService
    {
        void Checkout(IEnumerable<int> itemIDs, int memberId, string promoCode, DateTime when);
        //int CalculateDiscountForMemberBirthday(DateTime birthday, Member member);
        //int CalculateDiscountForPromoCode(string promoCode, DateTime refDateTime);
        //decimal CalculateTotalPayable(int birthdayDiscountPercentage, int promoDiscountPercentage, IEnumerable<Item> items);
    }
}