using AutoMail.Models.Entities;
using AutoMail.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AutoMail.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<EmailConfiguration> EmailConfigurations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 指定所有实体的模式
            modelBuilder.HasDefaultSchema("AutoMail");
        }
    }
}
