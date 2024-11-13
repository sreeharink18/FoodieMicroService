using Foodie.Service.ShoppingCartAPI.Model.DTO;

namespace Foodie.Service.EmailApi.ExternalServices
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
    }
}
