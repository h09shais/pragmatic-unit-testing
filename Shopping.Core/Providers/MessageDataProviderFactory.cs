using System;
using System.Linq;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Services;

namespace Shopping.Core.Providers
{
    public class MessageDataProviderFactory
    {
        public static MessageDataProvider Create()
        {
            return new MessageDataProvider
            {
                FindUserById = UserRepository.FindById,
                
                FindReceiverById = ReceiverRepository.FindById,
                
                MessageIsEmpty = string.IsNullOrEmpty,
                
                MessageHasCurseWords = message =>
                    BlockListRepository.Words().
                        Any(blockedWords => 
                            message.Contains(blockedWords, StringComparison.InvariantCultureIgnoreCase)),

                UserIsBlackListed = userId => 
                    BlockListRepository.Users().Any(user => user.Id == userId),

                ReceiverBlockUser = (receiverId, userId) => 
                    BlockListRepository.Users().Any(user => user.Id == userId) 
                    && BlockListRepository.Receivers().Any(receiver => receiver.Id == receiverId),

                SaveMessage = SaveMessage,

                NotifyReceiver = NotifyReceiver
            };
        }

        private static void SaveMessage(string message)
        {
            throw new NotImplementedException();
        }

        private static void NotifyReceiver(int receiverId, int userId, string message)
        { 
            LoggingService.Log(LogLevel.Info, $"User {userId} send message to Receiver {receiverId}"); 
            //NotificationService.Notify(FindUserById(userId), FindReceiverById(receiverId), message);
        }
    }
}
