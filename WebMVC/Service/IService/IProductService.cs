using WebMVC.Models;
using WebMVC.Models.DTO.ProductDTOFolder;

namespace WebMVC.Service.IService
{
    public interface IProductService
    {
        Task<ApiResponse> GetAllProductAsync();
        Task<ApiResponse> GetProductByIdAsync(int productId);
        Task<ApiResponse> CreateProductAsync(ProductCreateDto productDTO);
        Task<ApiResponse> UpdateProductAsync(ProductUpdateDto productDto, int productId);
        Task<ApiResponse> DeleteProductAsync(int productId);
    }
}
