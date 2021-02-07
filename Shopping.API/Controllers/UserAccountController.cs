using Microsoft.AspNetCore.Mvc;
using Shopping.Core.Repositories;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class UserAccountController : Controller
    {
        [HttpPost]
        public void ChangePassword(ChangePasswordRequest request)
        {
            var userAccountRepository = new UserAccountRepository();
            UserAccountService.ChangePassword(
                request,
                ContextualUserId,
                userAccountRepository.GetById,
                userAccountRepository.SetPassword);
        }

        private int ContextualUserId = 64564;
    }
}
