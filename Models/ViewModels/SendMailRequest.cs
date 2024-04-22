namespace AutoMail.Models.ViewModels
{
    public class SendMailRequest
    {
        public int EmailConfigID { get; set; }
        public string ReceiveEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
