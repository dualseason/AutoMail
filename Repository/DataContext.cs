using AutoMail.Models.Entities;
using AutoMail.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AutoMail.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<EmailConfiguration> EmailConfigurations { get; set; } = null!;
    }
}
