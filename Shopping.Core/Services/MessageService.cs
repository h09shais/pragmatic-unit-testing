using Shopping.Core.Providers;
using Shopping.Core.Requests;

namespace Shopping.Core.Services
{
    public class MessageService
    {
        public static void Send(
            MessageRequest request,
            DataProvider dataProvider
        )
        {
            MessagingValidation.MessageIsNotEmpty(dataProvider.MessageIsEmpty(request.Message));
            MessagingValidation.MessageHasNoCurseWords(dataProvider.MessageHasCurseWords(request.Message));
            MessagingValidation.UserIsNotBlackListed(dataProvider.UserIsBlackListed(request.UserId));
            MessagingValidation.ReceiverDoesNotBlockUser(dataProvider.ReceiverBlockUser(request.ReceiverId, request.UserId));

            dataProvider.SaveMessage(request.Message);
            dataProvider.NotifyReceiver(request.UserId, request.ReceiverId, request.Message);
        }
    }
}
