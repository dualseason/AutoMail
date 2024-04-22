using AutoMail.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMail.Models.Entitys
{
    [Table("email_configuration")]
    public class EmailConfiguration : BaseEntity
    {
        // SMTP 服务器的地址或主机名，用于发送邮件。
        [Required]
        public required string SmtpServer { get; set; }

        [Required]
        // SMTP 服务器的端口号，通常是 25、465 或 587。
        public int SmtpPort { get; set; }

        [Required]
        // 用于身份验证的用户名，可能是你的邮箱地址。
        public required string UserName { get; set; }

        [Required]
        // 用于身份验证的密码。
        public required string Password { get; set; }

        // 指示是否启用 SSL 加密来保护 SMTP 连接。通常，如果 SMTP 服务器要求加密连接，则应该启用它。
        public bool EnableSSL { get; set; }

        // 指示是否是默认的邮箱配置。当有多个邮箱配置时，可能会需要指定一个默认配置，用于发送邮件。
        public bool Default { get; set; }

        // 外键属性，表示与 User 表的关联
        public int UserId { get; set; }

        // 导航属性，表示与 User 表的关联
        public ApplicationUser? User { get; set; }
    }
}
