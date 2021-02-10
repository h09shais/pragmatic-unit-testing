using NUnit.Framework;
using Shopping.Core.Models;

namespace Shopping.Test
{
    class MemberDiscountTests
    {
        [Theory]
        [TestCase(MemberType.Diamond, 10)]
        [TestCase(MemberType.Gold, 5)]
        [TestCase(MemberType.Respected, 0)]
        public void Gets_discounts_per_type_of_membership(
            MemberType memberType,
            int expectedDiscount)
        {
            var member = new Membership { Type = memberType };
            var actual = MemberDiscount.GetInPercentage(member);
            Assert.AreEqual(actual, expectedDiscount);
        }
    }
}
