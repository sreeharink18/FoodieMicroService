using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models.DTO.ProductDTOFolder
{
    public class ProductCreateDto
    {
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
