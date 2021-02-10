using System;
using Moq;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class MessagingServiceIntegrationTests
    {
        [Test]
        public void If_message_contains_block_word_Then_sending_messages_should_throw_exception()
        {
            var dataProvider = new DataProvider
            {
                FindUserById = userId => new User { Id = 1 },
                FindReceiverById = receiverId => new Receiver { Id = 2},
                MessageIsEmpty = message => false,
                MessageHasCurseWords = message => true,
                UserIsBlackListed = userId => false,
                ReceiverBlockUser = (receiverId, userId) => false
            };

            Assert.Throws<InternalException>(() =>
                MessageService.Send(
                    new MessageRequest {}, 
                    dataProvider));
        }

        [Test]
        public void If_user_not_blocked_And_receiver_not_blocked_And_message_contains_no_block_word_Then_sending_messages_should_send()
        {
            var request = new MessageRequest
            {
                UserId = 1,
                ReceiverId = 2,
                Message = "Test!"
            };

            var dataProvider = new DataProvider
            {
                FindUserById = userId => new User { Id = 1 },
                FindReceiverById = receiverId => new Receiver { Id = 2 },
                MessageIsEmpty = message => false,
                MessageHasCurseWords = message => false,
                UserIsBlackListed = userId => false,
                ReceiverBlockUser = (receiverId, userId) => false
            };

            var saveMessageMock = new Mock<Action<string>>();
            var notifyReceiverMock = new Mock<Action<int, int, string>>();

            dataProvider.SaveMessage = saveMessageMock.Object;
            dataProvider.NotifyReceiver = notifyReceiverMock.Object;

            MessageService.Send(
                request,
                dataProvider);

            notifyReceiverMock.Verify(item => item(1, 2, request.Message), Times.Once);
        }
    }
}
