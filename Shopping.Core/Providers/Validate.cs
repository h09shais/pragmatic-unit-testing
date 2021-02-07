using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.Core.Models;

namespace Shopping.Core.Providers
{
    public class Validate
    {
        public static bool UserIsNotBlocked(User user, IEnumerable<User> blockList)
        {
            if (blockList.Any(blockedUser => user.Id == blockedUser.Id))
            {
                throw new InternalException("User is blocked!");
            }

            return true;
        }

        public static bool ReceiverIsNotBlocked(Receiver receiver, IEnumerable<Receiver> blockList)
        {
            if (blockList.Any(blockedReceiver => receiver.Id == blockedReceiver.Id))
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
