using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shopping.Core.Models;
using Shopping.Core.Services;

namespace Shopping.Test
{
    public class MessagingServiceIntegrationTests
    {
        public static object[][] MessagingTestCases = {
            new object[]
            {
                "basic send operation",
                new Sender { Name = "sender1"},
                new Receiver { Name = "receiver1" },
                "Hello",
                new Sender[] {},
                new string[] {},
                true
            },
            new object[]
            {
                "blocked sender",
                new Sender { Name = "sender2"},
                new Receiver { Name = "receiver2" },
                "Hello",
                new[] { new Sender { Name = "sender2" } },
                new string[] {},
                false
            },
            new object[]
            {
                "blocked sender on receiver",
                new Sender { Name = "sender3"},
                new Receiver { Name = "Receiver3" },
                "Hello",
                new[] { new Sender { Name = "sender3" } },
                new string[] {},
                false
            },
            new object[]
            {
                "blocked word",
                new Sender { Name = "sender4"},
                new Receiver { Name = "receiver4" },
                "Hello",
                new Sender[] {},
                new[] { "Hello" },
                false
            },
            new object[]
            {
                "blocked word with lower-case",
                new Sender { Name = "sender5" },
                new Receiver { Name = "receiver5" },
                "Hello",
                new Sender[] {},
                new[] { "hello" },
                false
            },
        };

        [Theory]
        [TestCaseSource(nameof(MessagingTestCases))]
        public void sending_messages(
           string scenario,
           Sender sender,
           Receiver receiver,
           string message,
           IEnumerable<Sender> senderBlockList,
           IEnumerable<string> wordBlockList,
           bool expectedSend)
        {
            var sendMessageMock = new Mock<Action<Sender, Receiver, string>>();

            MessageService.Send(
                () => sender,
                () => receiver,
                message,
                sendMessageMock.Object);
            
            if (expectedSend)
            {
                sendMessageMock.Verify(r => r(sender, receiver, message), Times.Once, failMessage: scenario);
            }
            else
            {
                sendMessageMock.Verify(r => r(sender, receiver, message), Times.Never, failMessage: scenario);
            }
        }
    }
}
