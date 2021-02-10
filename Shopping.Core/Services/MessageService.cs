using Shopping.Core.Providers;
using Shopping.Core.Requests;
using Shopping.Core.Validations;

namespace Shopping.Core.Services
{
    public class MessageService
    {
        public static void Send(
            MessageRequest request,
            MessageDataProvider dataProvider
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
