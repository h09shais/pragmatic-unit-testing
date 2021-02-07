using Microsoft.AspNetCore.Mvc;
using Shopping.Core.Models;
using Shopping.Core.Repositories;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class MessageController : Controller
    {
        public void Send(MessageRequest request)
        {
            var blockListRepository = new BlockListRepository();
            var userRepository = new UserRepository();
            var receiverRepository = new ReceiverRepository();

            MessageService.Send(
                request,
                blockListRepository,
                userRepository.FindById,
                receiverRepository.FindById,
                (user, receiver, message) => {
                    LoggingService.Log(LogLevel.Info, $"User {user.Id} send message to Receiver {receiver.Id}");
                    NotificationService.Notify(user, receiver, message);
                });
        }
    }
}
