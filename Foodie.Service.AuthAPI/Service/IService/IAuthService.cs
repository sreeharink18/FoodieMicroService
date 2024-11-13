using Foodie.Service.AuthAPI.Models.DTO;

namespace Foodie.Service.AuthAPI.Service.IService
{
	public interface IAuthService
	{
		Task<string> Register(RegisterRequestDto requestDto);
		Task<LoginResponseDto> Login(LoginRequestDto requestDto);
		Task<bool> AssignRole(string email, string role);
	}
}
