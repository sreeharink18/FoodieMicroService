using Foodie.Service.ShoppingCartAPI.Model;
using Foodie.Service.ShoppingCartAPI.Model.DTO;
using Foodie.Service.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Foodie.Service.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory) { 
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product/getAllProducts");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ApiResponse>(Convert.ToString(apiContent));
            if (resp!=null && resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }
    }
}
