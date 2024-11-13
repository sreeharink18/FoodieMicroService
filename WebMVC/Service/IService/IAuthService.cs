using WebMVC.Models;
using WebMVC.Models.DTO.AuthDTOFolder;

namespace WebMVC.Service.IService
{
	public interface IAuthService
	{
		Task<ApiResponse> LoginAsync(LoginRequestDto loginRequestDto);
		Task<ApiResponse> RegisterAsync(RegisterRequestDto registerRequestDto);
		Task<ApiResponse> AssignRoleAsync(RegisterRequestDto registerRequestDto);
	}
}
