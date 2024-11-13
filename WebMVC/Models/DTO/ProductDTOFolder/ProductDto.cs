namespace WebMVC.Models.DTO.ProductDTOFolder
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string CategoryName { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public int Count { get; set; } = 1;
    }
}
