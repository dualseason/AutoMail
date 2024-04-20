using AutoMail.Models.Entitys;
using System.ComponentModel.DataAnnotations;

namespace AutoMail.Models.Entities
{
    public class EmailSendRecord: BaseEntity
    {
        [Required]
        public required string Subject { get; set; }
        [Required]
        public required string Body { get; set; }
        [Required]
        public required string Sender { get; set; }
        [Required]
        public required string Recipient { get; set; }
        public DateTime SentAt { get; set; }
        // 其他邮件发送记录相关的属性，根据需要添加

        public EmailSendRecord()
        {
            SentAt = DateTime.UtcNow;
        }
    }
}
