namespace Foodie.Services.CouponAPI.Models.DTO.InComing
{
    public class UpdateRequestCouponDTO
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }

}
