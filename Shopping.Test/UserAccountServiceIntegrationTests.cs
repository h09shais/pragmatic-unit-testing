using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.Test
{
    class UserAccountServiceIntegrationTests
    {
        [Test]
        public void If_old_password_does_not_match_Then_throws_BadOldPasswordError()
        {
            var userId = 1234;

            Assert.Throws<InternalException>(() =>
                UserAccountService.ChangePassword(
                    new ChangePasswordRequest
                    {
                        OldPassword = "foo"
                    },
                    userId,
                    _ =>
                        new UserAccount
                        {
                            Password = "bar",
                            Enabled = true
                        },
                    (password, id) => { return; }));
        }
    }
}
