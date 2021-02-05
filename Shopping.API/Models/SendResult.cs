namespace Shopping.API.Models
{
    public class SendResult
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
    }
}