using System;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Models;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        public void Checkout(BuyRequest request)
        {
            _shoppingService.Checkout(request.ItemIDs, ContextualMemberID, request.PromoCode, DateTime.Now);
        }

        private int ContextualMemberID = 20190620;
    }
}
