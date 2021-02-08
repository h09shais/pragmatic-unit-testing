using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class MessagingServiceIntegrationTests
    {
        public static object[][] MessageContainsBlockWord = {
            new object[]
            {
                new MessageRequest
                {
                    Message =  "basic send operation",
                    ReceiverId = 12,
                    UserId = 13
                },
                new User { Id = 13, Name = "user"},
                new Receiver { Id = 12, Name = "receiver" },
                new List<User>
                {
                    new User
                    {
                        Id = 1
                    },
                    new User
                    {
                        Id = 2
                    },
                },
                new List<Receiver>
                {
                    new Receiver
                    {
                        Id = 1
                    },
                    new Receiver
                    {
                        Id = 2
                    },
                },
                new List<string>{"basic", "test"}
            }
        };

        [Theory]
        [TestCaseSource(nameof(MessageContainsBlockWord))]
        public void If_message_contains_block_word_Then_sending_messages_should_throw_exception(
            MessageRequest request, 
            User user, 
            Receiver receiver,
            List<User> userBlockList,
            List<Receiver> receiverBlockList,
            List<string> wordBlockList)
        {
            var repository = new BlockListRepository
            {
                Users = () => userBlockList,
                Receivers = () => receiverBlockList,
                Words = () => wordBlockList
            };

            Assert.Throws<InternalException>(() =>
                MessageService.Send(
                    request,
                    repository,
                    _ => user,
                    _ => receiver,
                    ((from, to, message) => { return;})
                ));
        }

        public static object[][] UserNotBlockedAndReceiverNotBlockedAndMessageContainsNoBlockWord = {
            new object[]
            {
                new MessageRequest
                {
                    Message =  "basic send operation",
                    ReceiverId = 12,
                    UserId = 13
                },
                new User { Id = 13, Name = "user"},
                new Receiver { Id = 12, Name = "receiver" },
                new List<User>
                {
                    new User
                    {
                        Id = 1
                    },
                    new User
                    {
                        Id = 2
                    },
                },
                new List<Receiver>
                {
                    new Receiver
                    {
                        Id = 1
                    },
                    new Receiver
                    {
                        Id = 2
                    },
                },
                new List<string>{"good", "test"}
            }
        };

        [Theory]
        [TestCaseSource(nameof(UserNotBlockedAndReceiverNotBlockedAndMessageContainsNoBlockWord))]
        public void If_user_not_blocked_And_receiver_not_blocked_And_message_contains_no_block_word_Then_sending_messages_should_send(
            MessageRequest request,
            User user,
            Receiver receiver,
            List<User> userBlockList,
            List<Receiver> receiverBlockList,
            List<string> wordBlockList)
        {
            var repository = new BlockListRepository
            {
                Users = () => userBlockList,
                Receivers = () => receiverBlockList,
                Words = () => wordBlockList
            };
            var saveAndNotifyMock = new Mock<Action<User, Receiver, string>>();

            MessageService.Send(
                request,
                repository,
                _ => user,
                _ => receiver,
                saveAndNotifyMock.Object
            );

            saveAndNotifyMock.Verify(item => item(user, receiver, request.Message), Times.Once);
        }
    }
}
