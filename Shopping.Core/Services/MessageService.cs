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
            SendRequest request, 
            BlockListRepository blockListRepository, 
            Func<int, Sender> findSender, 
            Func<int, Receiver> findReceiver, 
            Action<Sender, Receiver, string> saveAndNotify)
        {
            var sender = findSender(request.SenderId);
            var senderIsNotBlocked = Validate.SenderIsNotBlocked(sender, blockListRepository.Senders());

            var receiver = findReceiver(request.ReceiverId);
            var receiverIsNotBlocked = Validate.ReceiverIsNotBlocked(receiver, blockListRepository.Receivers());

            var messageIsNotBlocked = Validate.MessageIsNotBlocked(request.Message, blockListRepository.Words());

            if (senderIsNotBlocked && receiverIsNotBlocked && messageIsNotBlocked)
            {
                saveAndNotify(sender, receiver, request.Message);
            }
        }
    }
}
