using SqlSugar;

namespace AutoMail.Models.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; } = false;

        [Navigate(NavigateType.OneToMany, nameof(EmailConfiguration.UserId), nameof(ID))]
        public List<EmailConfiguration>? EmailConfigurations { get; set; }
    }
}
