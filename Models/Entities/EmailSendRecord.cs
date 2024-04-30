namespace AutoMail.Models.Entities
{
    public class EmailSendRecord: BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public DateTime SentAt { get; set; }
        // 其他邮件发送记录相关的属性，根据需要添加

        public EmailSendRecord()
        {
            SentAt = DateTime.UtcNow;
        }
    }
}
