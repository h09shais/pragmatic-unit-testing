using System;
using Shopping.Core.Models;
using Shopping.Core.Providers;

namespace Shopping.Core.Services
{
    public class MessageService
    {
        public static void Send(
            Lazy<Sender> findSender, 
            Lazy<Receiver> findReceiver, 
            string message,
            Action<Sender, Receiver, string> saveAndNotify)
        {
            /*
             * There can be alternatives to the validSender/Receiver pattern,
             * one of them is letting the validators throw exceptions directly
             * and return void so the Send method can carry on without a care.
             */
            
            var sender = findSender.Value;
            var validSender = Validate.SenderIsNotBlocked(sender);

            var validMessage = Validate.MessageIsNotBlocked(message);

            var receiver = findReceiver.Value;
            var validReceiver = Validate.ReceiverIsNotBlocked(receiver);

            if (validSender && validReceiver && validMessage)
            {
                saveAndNotify(sender, receiver, message);
            }
        }
    }
}
