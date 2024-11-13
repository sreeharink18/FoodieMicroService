using AutoMapper;
using Foodie.Services.CouponAPI.Models;
using Foodie.Services.CouponAPI.Models.DTO.InComing;
using Foodie.Services.CouponAPI.Models.DTO.OutGoing;

namespace Foodie.Services.CouponAPI.ExternalService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Coupon, CouponResponseDTO>();
            CreateMap<CreateRequestCouponDTO, Coupon>();
            CreateMap<UpdateRequestCouponDTO, Coupon>();
        }
    }
}
