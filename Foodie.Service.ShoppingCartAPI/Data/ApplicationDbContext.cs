using Foodie.Service.ShoppingCartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Service.ShoppingCartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

       public DbSet<CartHeader> CartHeaders { get; set; }
       public DbSet<CartDetails> CartDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
