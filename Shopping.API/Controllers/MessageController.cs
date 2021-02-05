using Microsoft.AspNetCore.Mvc;
using Shopping.API.Models;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class MessageController : Controller
    {
        public void Send(SendResult request)
        {
            MessageService.Send(
                () => {
                    var senderId = request.SenderId;
                    var sender = SenderRepository.FindById(senderId);
                    if (sender == null)
                    {
                        throw new NotFoundException(senderId);
                    }
                    return sender;
                },
                () => {
                    var receiverId = request.ReceiverId;
                    var receiver = ReceiverRepository.FindById(receiverId);
                    if (receiver == null)
                    {
                        throw new NotFoundException(receiverId);
                    }
                    return receiver;
                },
                request.Message,
                (sender, receiver, message) => {
                    LoggingService.Log(LogLevel.Info, $"Sender {sender.Id} send message to Receiver {receiver.Id}");
                    NotificationService.Notify(sender, receiver, message);
                });
        }
    }
}
