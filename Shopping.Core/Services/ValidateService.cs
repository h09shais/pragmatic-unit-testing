using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Core.Models;

namespace Shopping.Core.Services
{
    public class ValidateService
    {
        public static bool IsNotBlocked(
            Sender sender, 
            IEnumerable<Sender> senders,
            Receiver receiver, 
            IEnumerable<Receiver> receivers,
            string message, 
            IEnumerable<string> words
        )
        {
            return SenderIsNotBlocked(sender, senders) &&
                   ReceiverIsNotBlocked(receiver, receivers) &&
                   MessageIsNotBlocked(message, words);
        }

        public static bool SenderIsNotBlocked(Sender sender, IEnumerable<Sender> senders)
        {
            return senders.Any(blockedSender => sender.Name == blockedSender.Name);
        }

        public static bool ReceiverIsNotBlocked(Receiver receiver, IEnumerable<Receiver> receivers)
        {
            return receivers.Any(blockedReceiver => receiver.Name == blockedReceiver.Name);
        }

        public static bool MessageIsNotBlocked(string message, IEnumerable<string> words)
        {
            return words.Any(word => message.Contains(word, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}