using Foodie.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Services.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon { 
                CouponId = 1,
                CouponCode = "50OFF",
                DiscountAmount = 50,
                MinAmount = 200
            });

        }
    }
}
