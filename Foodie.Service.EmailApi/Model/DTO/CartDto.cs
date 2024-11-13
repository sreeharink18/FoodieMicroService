using Foodie.Service.EmailApi.Model.DTO;

namespace Foodie.Service.ShoppingCartAPI.Model.DTO
{
    public class CartDto
    {
        public CartHeaderDto CartHeaderDto { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetailsDto { get; set; }
    }
}
