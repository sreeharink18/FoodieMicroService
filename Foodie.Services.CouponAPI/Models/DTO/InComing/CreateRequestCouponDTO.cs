using System.ComponentModel.DataAnnotations;

namespace Foodie.Services.CouponAPI.Models.DTO.InComing
{
    public class CreateRequestCouponDTO
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
