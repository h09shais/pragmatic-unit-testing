namespace Shopping.Core.Requests
{
    public class MessageRequest
    {
        public int UserId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
    }
}