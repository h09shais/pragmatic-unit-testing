using Microsoft.AspNetCore.Mvc;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class MessageController : Controller
    {
        public void Send(SendRequest request)
        {
            var blockListRepository = new BlockListRepository();
            var senderRepository = new SenderRepository();
            var receiverRepository = new ReceiverRepository();

            MessageService.Send(
                request,
                blockListRepository,
                senderRepository.FindById,
                receiverRepository.FindById,
                (sender, receiver, message) => {
                    LoggingService.Log(LogLevel.Info, $"Sender {sender.Id} send message to Receiver {receiver.Id}");
                    NotificationService.Notify(sender, receiver, message);
                });
        }
    }
}
