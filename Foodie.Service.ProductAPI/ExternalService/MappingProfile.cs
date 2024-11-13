using AutoMapper;
using Foodie.Service.ProductAPI.Models;
using Foodie.Service.ProductAPI.Models.DTO;

namespace Foodie.Service.ProductAPI.ExternalService
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
