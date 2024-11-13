using AutoMapper;
using Foodie.Service.ShoppingCartAPI.Model;
using Foodie.Service.ShoppingCartAPI.Model.DTO;


namespace Foodie.Service.ShoppingCartAPI.ExternalService
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            CreateMap<CartHeaderDto, CartHeader>();
            CreateMap<CartHeader, CartHeaderDto>();

            CreateMap<CartDetails, CartDetailsDto>();
            CreateMap<CartDetailsDto, CartDetails>();
        }
    }
}
