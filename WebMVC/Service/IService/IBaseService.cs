using WebMVC.Models;

namespace WebMVC.Service.IService
{
    public interface IBaseService
    {
        Task<ApiResponse?> SendAsync(RequestDTO requestDTO, bool withBearer = true);
    }
}
