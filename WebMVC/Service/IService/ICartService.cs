using WebMVC.Models;
using WebMVC.Models.DTO.CartDTOFolder;
using WebMVC.Models.DTO.CouponDTOFolder;

namespace WebMVC.Service.IService
{
    public interface ICartService
    {
        Task<ApiResponse> GetCartAsync(string userId);
        Task<ApiResponse> CartUpsertAsync(CartDto cartDto);
        Task<ApiResponse> RemoveCartAsync(int cartdetailId);
        Task<ApiResponse> EmailCart(CartDto cartDto);


    }
}
