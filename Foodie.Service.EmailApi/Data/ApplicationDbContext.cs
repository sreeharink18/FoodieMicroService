
using Foodie.Service.EmailApi.Model;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Service.EmailApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EmailLogger> EmailLoggers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
