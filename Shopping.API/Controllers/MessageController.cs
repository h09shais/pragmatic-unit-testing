using Microsoft.AspNetCore.Mvc;
using Shopping.Core.Providers;
using Shopping.Core.Requests;
using Shopping.Core.Services;

namespace Shopping.API.Controllers
{
    public class MessageController : Controller
    {
        public void Send(MessageRequest request)
        {
            MessageService.Send(request, new MessageDataProvider());
        }
    }
}
