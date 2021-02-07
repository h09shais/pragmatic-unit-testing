using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Core.Models;

namespace Shopping.Core.Providers
{
    public class Validate
    {
        public static bool SenderIsNotBlocked(Sender sender, IEnumerable<Sender> blockList)
        {
            if (blockList.Any(blockedSender => sender.Name == blockedSender.Name))
            {
                throw new InternalException("Sender is blocked!");
            }

            return true;
        }

        public static bool ReceiverIsNotBlocked(Receiver receiver, IEnumerable<Receiver> blockList)
        {
            if (blockList.Any(blockedReceiver => receiver.Name == blockedReceiver.Name))
            {
                throw new InternalException("Receiver is blocked!");
            }

            return true;
        }

        public static bool MessageIsNotBlocked(string message, IEnumerable<string> blockList)
        {
            if (blockList.Any(blockedWords =>
                message.Contains(blockedWords, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new InternalException("Message is blocked!");
            }

            return true;
        }
    }
}
