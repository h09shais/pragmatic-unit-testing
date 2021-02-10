using System;
using Shopping.Core.Models;

namespace Shopping.Core.Services
{
    public class DataProvider
    {
        public Func<int, User> FindUserById { get; set; }

        public Func<int, Receiver> FindReceiverById { get; set; }

        public Func<string, bool> MessageIsEmpty { get; set; }

        public Func<string, bool> MessageHasCurseWords { get; set; }

        public Func<int, bool> UserIsBlackListed { get; set; }

        public Func<int, int, bool> ReceiverBlockUser { get; set; }

        public Action<string> SaveMessage { get; set; }

        public Action<int, int, string> NotifyReceiver { get; set; }
    }
}