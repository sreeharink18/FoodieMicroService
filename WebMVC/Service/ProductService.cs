using WebMVC.Models;
using WebMVC.Models.DTO.ProductDTOFolder;
using WebMVC.Service.IService;
using WebMVC.Utility;
using static WebMVC.Utility.SD;

namespace WebMVC.Service
{
    public class ProductService : IProductService
    {
        private IBaseService _baseService;
        public ProductService(IBaseService baseService) { 
            _baseService = baseService;
        }
        public async Task<ApiResponse> CreateProductAsync(ProductCreateDto productDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.POST,
                Data = productDTO,
                Url = SD.ProductAPIBase + "/api/product/createProduct"
            });
        }

        public async Task<ApiResponse> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/deleteProduct/" + productId
            });
        }

        public async Task<ApiResponse> GetAllProductAsync()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.GET, 
                Url = SD.ProductAPIBase + "/api/product/getAllProducts"
            });
        }

        public async Task<ApiResponse> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/getProductById/"+productId
            });
        }

        public async Task<ApiResponse> UpdateProductAsync(ProductUpdateDto productDto, int productId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product/updateProduct/" + productId
            });
        }
    }
}
