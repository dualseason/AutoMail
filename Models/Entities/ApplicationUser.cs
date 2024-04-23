using AutoMail.Models.Entitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMail.Models.Entities
{
    [Table("ApplicationUser")]
    public class ApplicationUser : BaseEntity
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; } = false;
    }
}
