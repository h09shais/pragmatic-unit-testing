using System;
using Shopping.Core.Models;
using Shopping.Core.Providers;

namespace Shopping.Core.Services
{
    public class MessageService
    {
        public static void Send(
            Func<Sender> findSender, 
            Func<Receiver> findReceiver, 
            string message,
            Action<Sender, Receiver, string> saveAndNotify)
        {
            //ValidateSender - The sender is not in a block list
            var sender = findSender();
            var validSender = Validate.Sender(sender);

            //ValidateMessage - Message is not empty and has no curse-words
            var validMessage = Validate.Message(message);

            //ValidateReceiver - The receiver exists, and does not block the sender
            var receiver = findReceiver();
            var validReceiver = Validate.Receiver(receiver);

            if (validSender && validReceiver && validMessage)
            {
                //Saves the message, and notifies the receiver
                saveAndNotify(sender, receiver, message);
            }
        }
    }
}
