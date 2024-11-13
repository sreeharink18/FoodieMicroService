namespace WebMVC.Models.DTO.CartDTOFolder
{
    public class CartDto
    {
        public CartHeaderDto CartHeaderDto { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetailsDto { get; set; }
    }
}
