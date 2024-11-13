using WebMVC.Models;
using WebMVC.Models.DTO.CouponDTOFolder;
using WebMVC.Service.IService;
using WebMVC.Utility;
using static WebMVC.Utility.SD;

namespace WebMVC.Service
{
    public class CouponService :ICouponService
    {
        private IBaseService _baseService;
        public CouponService(IBaseService baseService) { 
            _baseService = baseService;
        }

        public async Task<ApiResponse> CreateCouponAsync(CreateRequestCouponDTO couponDTO)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.POST,
                Data = couponDTO,
                Url = SD.CouponAPIBase + "/api/Coupon/createCoupon"
            });
        }

        public async Task<ApiResponse> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.DELETE,
                Url = $"{SD.CouponAPIBase}/api/Coupon/deleteCoupon/{couponId}"
            });
        }

        public async Task<ApiResponse> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDTO { 
                ApiType = ApiType.GET,
                Url = SD.CouponAPIBase + "/api/Coupon/getAllCoupons"
            });

        }

        public async Task<ApiResponse> GetCouponByCouponCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.GET,
                Url = $"{SD.CouponAPIBase}/api/Coupon/getCouponByCouponCode/{couponCode}"
            });
        }

        public async Task<ApiResponse> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.GET,
                Url = SD.CouponAPIBase + "/api/Coupon/getCouponById/" + couponId
            });
        }

        public async Task<ApiResponse> UpdateCouponAsync(UpdateRequestCouponDTO couponDto, int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                ApiType = ApiType.PUT,
                Data = couponDto,
                Url = $"{SD.CouponAPIBase}/api/Coupon/updateCoupon/{couponId}"
            });
        }
    }
}
