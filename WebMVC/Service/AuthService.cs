using WebMVC.Models;
using WebMVC.Models.DTO.AuthDTOFolder;
using WebMVC.Service.IService;
using WebMVC.Utility;
using static WebMVC.Utility.SD;

namespace WebMVC.Service
{
	public class AuthService : IAuthService
	{
		private IBaseService _baseService;
		public AuthService(IBaseService baseService) { 
			_baseService = baseService;
		}
		public Task<ApiResponse> AssignRoleAsync(RegisterRequestDto registerRequestDto)
		{
			return _baseService.SendAsync(new RequestDTO
			{
				ApiType = ApiType.POST,
				 Data = registerRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/assignRole"

			});

		}

		public Task<ApiResponse> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return _baseService.SendAsync(new RequestDTO
			{
				ApiType = ApiType.POST,
				Data = loginRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/login"

			},withBearer:false);
		}
		public Task<ApiResponse> RegisterAsync(RegisterRequestDto registerRequestDto)
		{
			return _baseService.SendAsync(new RequestDTO
			{
				ApiType = ApiType.POST,
				Data = registerRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/register"

			},withBearer:false);
		}
	}
}
