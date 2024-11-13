using WebMVC.Models;
using WebMVC.Models.DTO.CartDTOFolder;
using WebMVC.Service.IService;
using WebMVC.Utility;
using static WebMVC.Utility.SD;

namespace WebMVC.Service
{
    public class CartService : ICartService
    {
        private IBaseService _baseService;
        public CartService(IBaseService baseService) { 
            _baseService = baseService;
        }
        public async Task<ApiResponse> CartUpsertAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/CartUpsert"

            });
        }

        public async Task<ApiResponse> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"

            });
        }

        public async Task<ApiResponse> GetCartAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/getCart/" + userId
            });
        }

        public async Task<ApiResponse> RemoveCartAsync(int cartdetailId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.POST,
                Data = cartdetailId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart"

            });
        }
    }
}
