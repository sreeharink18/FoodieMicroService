using System.ComponentModel.DataAnnotations;

namespace Foodie.Service.ProductAPI.Models.DTO
{
    public class ProductUpdateDto
    {

        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
