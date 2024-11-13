using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodie.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class addSeedDataCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 1, "50OFF", 50.0, 200 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);
        }
    }
}
