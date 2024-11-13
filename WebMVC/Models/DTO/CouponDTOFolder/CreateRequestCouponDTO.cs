using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models.DTO.CouponDTOFolder
{
    public class CreateRequestCouponDTO
    {
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        [Required]
        public int MinAmount { get; set; }
    }
}
