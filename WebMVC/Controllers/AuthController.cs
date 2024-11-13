using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebMVC.Models;
using WebMVC.Models.DTO.AuthDTOFolder;
using WebMVC.Service.IService;
using WebMVC.Utility;

namespace WebMVC.Controllers
{
	public class AuthController : Controller
	{
		private IAuthService _authService;
		private ITokenProvider _tokenProvider;
		public AuthController(IAuthService authService,ITokenProvider tokenProvider) {
			_authService = authService;
			_tokenProvider = tokenProvider;
		}
		
		public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            ApiResponse response = await _authService.LoginAsync(model);
           

            if (response != null && response.IsSuccess)
            {
				
				LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
				_tokenProvider.SetToken(loginResponse.Token);
				await SignInUser(loginResponse);
                TempData["success"] = "Login Successfully completed";
                return RedirectToAction("Index", "Home");
            }
			else
			{
                TempData["error"] = response.ErrorMessage;
                return View(model);
			}
           
        }
        public IActionResult Register()
		{
			var roleList = new List<SelectListItem>()
			{
				new SelectListItem(){Text=SD.RoleAdmin ,Value= SD.RoleAdmin},
				new SelectListItem(){Text=SD.RoleCustomer,Value= SD.RoleCustomer}
			};
			ViewBag.RoleList = roleList;
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto model)
        {
			ApiResponse response = await _authService.RegisterAsync(model);
			ApiResponse assignRoleResponse;

            if (response != null && response.IsSuccess) {

				if (string.IsNullOrEmpty(model.Role))
				{
					model.Role = SD.RoleCustomer;
				}
                assignRoleResponse = await _authService.AssignRoleAsync(model);
                if (assignRoleResponse != null && assignRoleResponse.IsSuccess) {
					TempData["success"] = "Register Successfully";
					return RedirectToAction(nameof(Login));
				}
			}
			else
			{
				TempData["error"] = response.ErrorMessage;
			}

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem(){Text=SD.RoleAdmin ,Value= SD.RoleAdmin},
                new SelectListItem(){Text=SD.RoleCustomer,Value= SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View(model);
        }

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			_tokenProvider.ClearToken();
			return RedirectToAction("Index","Home");
		}
		private async Task SignInUser(LoginResponseDto model)
		{
			var handler = new JwtSecurityTokenHandler();
			var jwt = handler.ReadJwtToken(model.Token);

			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
				jwt.Claims.FirstOrDefault(u=>u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principle = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

        }
	}
}
