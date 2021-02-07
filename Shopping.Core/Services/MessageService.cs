using System;
using Shopping.Core.Models;
using Shopping.Core.Providers;
using Shopping.Core.Repositories;
using Shopping.Core.Requests;

namespace Shopping.Core.Services
{
    public class MessageService
    {
        public static void Send(
            MessageRequest request, 
            BlockListRepository blockListRepository, 
            Func<int, User> findUser, 
            Func<int, Receiver> findReceiver, 
            Action<User, Receiver, string> saveAndNotify)
        {
            var user = findUser(request.UserId);
            var userIsNotBlocked = Validate.UserIsNotBlocked(user, blockListRepository.Users());

            var receiver = findReceiver(request.ReceiverId);
            var receiverIsNotBlocked = Validate.ReceiverIsNotBlocked(receiver, blockListRepository.Receivers());

            var messageIsNotBlocked = Validate.MessageIsNotBlocked(request.Message, blockListRepository.Words());

            if (userIsNotBlocked && receiverIsNotBlocked && messageIsNotBlocked)
            {
                saveAndNotify(user, receiver, request.Message);
            }
        }
    }
}
