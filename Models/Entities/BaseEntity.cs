using System.ComponentModel.DataAnnotations;

namespace AutoMail.Models.Entitys
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Optionally, add more common fields as needed
        // public string CreatedBy { get; set; }
        // public string UpdatedBy { get; set; }
        // public string IPAddress { get; set; }
        // public string UserAgent { get; set; }
        // public string ConcurrencyToken { get; set; }

        // Constructor to initialize common fields
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
    }
}
