using System;
using System.Linq;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Services;

namespace Shopping.Core.Providers
{
    public class MessageDataProviderFactory
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly ReceiverRepository _receiverRepository = new ReceiverRepository();
        private readonly BlockListRepository _blockListRepository = new BlockListRepository();

        MessageDataProvider Create()
        {
            return new MessageDataProvider
            {
                FindUserById = userId 
                    => _userRepository.FindById(userId),
                
                FindReceiverById = receiverId 
                    => _receiverRepository.FindById(receiverId),
                
                MessageIsEmpty = string.IsNullOrEmpty,
                
                MessageHasCurseWords = message =>
                    _blockListRepository.Words().
                        Any(blockedWords => 
                            message.Contains(blockedWords, StringComparison.InvariantCultureIgnoreCase)),

                UserIsBlackListed = userId => 
                    _blockListRepository.Users().Any(user => user.Id == userId),

                ReceiverBlockUser = (receiverId, userId) => 
                    _blockListRepository.Users().Any(user => user.Id == userId) 
                    && _blockListRepository.Receivers().Any(receiver => receiver.Id == receiverId),

                SaveMessage = SaveMessage,

                NotifyReceiver = NotifyReceiver
            };
        }

        private void SaveMessage(string message)
        {
            throw new NotImplementedException();
        }

        private void NotifyReceiver(int receiverId, int userId, string message)
        { 
            LoggingService.Log(LogLevel.Info, $"User {userId} send message to Receiver {receiverId}"); 
            //NotificationService.Notify(FindUserById(userId), FindReceiverById(receiverId), message);
        }
    }
}
