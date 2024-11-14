using Foodie.Service.AuthAPI.Models;
using Foodie.Service.AuthAPI.Models.DTO;
using Foodie.Service.AuthAPI.RabbitMQSender;
using Foodie.Service.AuthAPI.Service.IService;
using Foodie.Service.AuthAPI.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace Foodie.Service.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthApiController : ControllerBase
	{
		private IAuthService _authService;
		private IRabittMQAuthMessageSender _messageSender;
		public AuthApiController(IAuthService authService,IRabittMQAuthMessageSender messageSender)
		{
			_authService = authService;
			_messageSender = messageSender;

		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
		{
			var errorMessage = await _authService.Register(model);
			if (!string.IsNullOrEmpty(errorMessage)) { 
				return BadRequest(ApiResponseHelper.ErrorResponse(errorMessage));
			}
			_messageSender.SendMessage(model.Email, SD.RabbitMQQueueName);
			return Ok(ApiResponseHelper.SuccessResponse("User Register is sussessfull"));
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody]LoginRequestDto model)
		{
			LoginResponseDto loginResponseDto = await _authService.Login(model);
			if (loginResponseDto.User == null) {
				return BadRequest(ApiResponseHelper.ErrorResponse("Username or password is wrong"));
			}
			return Ok(ApiResponseHelper.SuccessResponse(loginResponseDto));
		}
		[HttpPost("assignRole")]
		public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDto model)
		{
			var isValid = await _authService.AssignRole(model.Email,model.Role.ToUpper());
			if (isValid== false)
			{
				return BadRequest(ApiResponseHelper.ErrorResponse("Something went wrong..."));
			}

			return Ok(ApiResponseHelper.SuccessResponse("Role assign is sussessfull"));
		}
	}
}
