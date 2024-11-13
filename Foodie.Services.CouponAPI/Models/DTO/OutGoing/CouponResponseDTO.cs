using System.ComponentModel.DataAnnotations;

namespace Foodie.Services.CouponAPI.Models.DTO.OutGoing
{
    public class CouponResponseDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
