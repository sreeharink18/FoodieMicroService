using Foodie.Service.AuthAPI.Data;
using Foodie.Service.AuthAPI.Models;
using Foodie.Service.AuthAPI.Models.DTO;
using Foodie.Service.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Service.AuthAPI.Service
{
	public class AuthService : IAuthService
	{
		private ApplicationDbContext _db;
		private UserManager<ApplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;
		private IJwtTokenGenerator _jwtTokenGenerator;
		public AuthService(ApplicationDbContext db, IJwtTokenGenerator jwtTokenGenerator,
			RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<bool> AssignRole(string email, string role)
		{
			ApplicationUser userFromDb = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
			if (userFromDb != null) {
				if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult()) { 
					_roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
				}
				await _userManager.AddToRoleAsync(userFromDb, role);
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
		{
			ApplicationUser userFromDb = await _db.ApplicationUsers.FirstOrDefaultAsync(u=>u.UserName == requestDto.UserName);
			if (userFromDb == null)
			{
				return new LoginResponseDto() { Token="",User=null};
			}
			bool isValid = await _userManager.CheckPasswordAsync(userFromDb, requestDto.Password);
			if (!isValid) {
				return new LoginResponseDto() { Token = "", User = null };
			}
			var roles = await _userManager.GetRolesAsync(userFromDb);	
			var token = _jwtTokenGenerator.GenerateToken(userFromDb,roles);
			UserDto userDto = new()
			{
				Email = userFromDb.Email,
				Id = userFromDb.Id,
				Name = userFromDb.Name,
				PhoneNumber = userFromDb.PhoneNumber
			};
			return new LoginResponseDto()
			{
				Token = token,
				User = userDto,
			};
		}

		public async Task<string> Register(RegisterRequestDto requestDto)
		{
			ApplicationUser user = new() { 
				Email = requestDto.Email,
				NormalizedEmail = requestDto.Email.ToUpper(),
				UserName = requestDto.Email,
				Name = requestDto.Name,
				PhoneNumber = requestDto.PhoneNumber,
				EmailConfirmed = true,
				TwoFactorEnabled = true,	
			};
			try
			{
				var result = await _userManager.CreateAsync(user,requestDto.Password);
				if (result.Succeeded)
				{
					var userToReturn = _db.ApplicationUsers.FirstOrDefault(user => user.Email == requestDto.Email);
					UserDto userDto = new()
					{
						Email = userToReturn.Email,
						Id = userToReturn.Id,
						Name = userToReturn.Name,
						PhoneNumber = userToReturn.PhoneNumber
					};
					return "";
				}
				else {
					return result.Errors.FirstOrDefault().Description;
				}
			}
			catch (Exception ex) {
				return ex.Message.ToString();
			}


		}
	}
}
