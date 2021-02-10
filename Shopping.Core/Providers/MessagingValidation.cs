using Shopping.Core.Models;

namespace Shopping.Core.Providers
{
    public class MessagingValidation
    {
        public static void MessageIsNotEmpty(bool isEmpty)
        {
            if (isEmpty)
            {
                throw new InternalException("Message is Empty");
            }
        }

        public static void ReceiverDoesNotBlockUser(bool isBlocked)
        {
            if (isBlocked)
            {
                throw new InternalException($"Receiver has blocked user");
            }
        }

        public static void MessageHasNoCurseWords(bool hasCurseWords)
        {
            if (hasCurseWords)
            {
                throw new InternalException($"Message has curse words");
            }
        }

        public static void UserIsNotBlackListed(bool isBlackListed)
        {
            if (isBlackListed)
            {
                throw new InternalException($"User is black listed");
            }
        }
    }
}
