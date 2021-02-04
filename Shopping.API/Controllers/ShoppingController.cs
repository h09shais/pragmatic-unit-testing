using System;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Models;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class ShoppingController : Controller
    {
        public void Checkout(BuyRequest request)
        {
            ShoppingService.Checkout(
                request.PromoCode,
                DateTime.Now,
                () => {
                    var member = MemberRepository.FindById(ContextualMemberID);
                    if (member == null)
                    {
                        throw new MemberNotFoundException(ContextualMemberID);
                    }
                    return member;
                },
                () => ItemRepository.FindByIds(request.ItemIds) ?? new Item[] { },
                total => {
                    LoggingService.Log(LogLevel.Info, $"Member {ContextualMemberID} charged {total}");
                    PaymentService.Charge(ContextualMemberID, total);
                });
        }

        private int ContextualMemberID = 20190620;
    }
}
