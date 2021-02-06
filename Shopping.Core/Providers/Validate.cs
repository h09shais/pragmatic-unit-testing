using System;
using System.Linq;
using Shopping.Core.Models;
using Shopping.Core.Repositories;

namespace Shopping.Core.Providers
{
    public class Validate
    {
        public static bool SenderIsNotBlocked(Sender sender)
        {
            var senders = BlockListRepository.Senders();
            return senders.Any(blockedSender => sender.Name == blockedSender.Name);
        }

        public static bool ReceiverIsNotBlocked(Receiver receiver)
        {
            var receivers = BlockListRepository.Receivers();
            return receivers.Any(blockedReceiver => receiver.Name == blockedReceiver.Name);
        }

        public static bool MessageIsNotBlocked(string message)
        {
            var words = BlockListRepository.Words();
            return words.Any(word => message.Contains(word, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
