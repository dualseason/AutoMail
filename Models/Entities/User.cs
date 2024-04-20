using AutoMail.Models.Entitys;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMail.Models.Entities
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Required]
        public required string UserName { get; set; }
        public int UserAge { get; set; }
        public string? UserEmail { get; set; }
        public string? UserGender { get; set; }
        public string? UserPhone { get; set; }
        [Required]
        public required string UserPassword { get; set; }
        public ICollection<EmailConfiguration>? EmailConfigurations { get; set; }
    }
}
