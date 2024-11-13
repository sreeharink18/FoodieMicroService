using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using WebMVC.Models;
using WebMVC.Models.DTO.CartDTOFolder;
using WebMVC.Service.IService;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartService;
        public CartController(ICartService cartService) { 
            _cartService = cartService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }
        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u=>u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ApiResponse response = await _cartService.GetCartAsync(userId);
            if(response !=null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new CartDto();
        } 
        public async Task<IActionResult> RemoveCartDetail(int cartDetailId)
        {
            ApiResponse response = await _cartService.RemoveCartAsync(cartDetailId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Upadate Cart Succefully";
                return RedirectToAction(nameof(CartIndex));
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            ApiResponse response = await _cartService.EmailCart(cart);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly";
                return RedirectToAction(nameof(CartIndex));
            }
            else
            {
                return View();
            }
        }
    }
}
