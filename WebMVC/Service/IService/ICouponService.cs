using WebMVC.Models;
using WebMVC.Models.DTO.CouponDTOFolder;

namespace WebMVC.Service.IService
{
    public interface ICouponService
    {
        Task<ApiResponse> GetAllCouponAsync();
        Task<ApiResponse> GetCouponByCouponCodeAsync(string couponCode);
        Task<ApiResponse> GetCouponByIdAsync(int couponId);
        Task<ApiResponse> CreateCouponAsync(CreateRequestCouponDTO couponDTO);
        Task<ApiResponse> UpdateCouponAsync(UpdateRequestCouponDTO couponDto,int couponId);
        Task<ApiResponse> DeleteCouponAsync(int couponId);


    }
}
