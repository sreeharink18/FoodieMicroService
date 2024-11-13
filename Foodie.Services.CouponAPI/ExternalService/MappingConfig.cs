using AutoMapper;
using Foodie.Services.CouponAPI.Models;
using Foodie.Services.CouponAPI.Models.DTO.InComing;
using Foodie.Services.CouponAPI.Models.DTO.OutGoing;

namespace Foodie.Services.CouponAPI.ExternalService
{
    public class MappingConfig
    {
        public static  MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponResponseDTO>();
                config.CreateMap<CreateRequestCouponDTO, Coupon>();
            });
            return mappingconfig;
        }
    }
}
