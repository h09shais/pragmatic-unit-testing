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
        public static object[][] MessagingTestCases = {
            new object[]
            {
               new SendRequest
               {
                   Message =  "basic send operation",
                   ReceiverId = 12,
                   SenderId = 13
               }
               , 
                new Sender { Id = 13, Name = "sender"},
                new Receiver { Id = 12, Name = "receiver" },
                new List<Sender>
                {
                    new Sender
                    {
                        Id = 1
                    },
                    new Sender
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
        [TestCaseSource(nameof(MessagingTestCases))]
        public void sending_messages(
            SendRequest request, 
            Sender sender, 
            Receiver receiver,
            List<Sender> senderBlockList,
            List<Receiver> receiverBlockList,
            List<string> wordBlockList)
        {
            var repository = new Mock<BlockListRepository>();
            repository.Setup(src => src.Senders()).Returns(senderBlockList);
            repository.Setup(src => src.Receivers()).Returns(receiverBlockList);
            repository.Setup(src => src.Words()).Returns(wordBlockList);


            Assert.Throws<InternalException>(() =>
                MessageService.Send(
                    request,
                    repository.Object,
                    _ => sender,
                    _ => receiver,
                    ((sender1, receiver1, message) => { return;})
                ));
        }
    }
}
