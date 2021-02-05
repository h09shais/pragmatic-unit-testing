using System;
using System.Linq;
using Shopping.Core.Models;
using Shopping.Core.Repositories;

namespace Shopping.Core.Providers
{
    public class Validate
    {
        public static bool Sender(Sender sender)
        {
            return BlockListRepository.Senders().Any(blockedSender => sender.Name == blockedSender.Name);
        }

        public static bool Receiver(Receiver receiver)
        {
            return BlockListRepository.Receivers().Any(blockedReceiver => receiver.Name == blockedReceiver.Name);
        }

        public static bool Message(string message)
        {
            return BlockListRepository.Words().Any(word => message.Contains(word, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
