namespace Shopping.Core.Requests
{
    public class SendRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
    }
}