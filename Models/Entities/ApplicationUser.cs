using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMail.Models.Entities
{
    [Table("application_user")]
    public class ApplicationUser : IdentityUser
    {
        // 可以添加自定义属性
        public int UserAge { get; set; }
        public string UserGender { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        protected ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
