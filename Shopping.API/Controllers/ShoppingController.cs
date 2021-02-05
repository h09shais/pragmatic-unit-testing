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
                    var member = MemberRepository.FindById(ContextualMemberId);
                    if (member == null)
                    {
                        throw new NotFoundException(ContextualMemberId);
                    }
                    return member;
                },
                () => ItemRepository.FindByIds(request.ItemIds) ?? new Item[] { },
                total => {
                    LoggingService.Log(LogLevel.Info, $"Member {ContextualMemberId} charged {total}");
                    PaymentService.Charge(ContextualMemberId, total);
                });
        }

        private int ContextualMemberId = 20190620;
    }
}
