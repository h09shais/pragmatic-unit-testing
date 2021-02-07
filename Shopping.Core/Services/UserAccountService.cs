using System;
using Shopping.Core.Models;
using Shopping.Core.Requests;

namespace Shopping.Core.Services
{
    public class UserAccountService
    {
        public static void ChangePassword(
            ChangePasswordRequest request, 
            int userId, 
            Func<int, UserAccount> getUserAccountById, 
            Action<string, int> setPasswordForUserId)
        {
            throw new NotImplementedException();
        }
    }
}