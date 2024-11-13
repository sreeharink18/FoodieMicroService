using Foodie.Service.ShoppingCartAPI.Model.DTO;

namespace Foodie.Service.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
