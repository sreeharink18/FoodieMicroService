using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using WebMVC.Models;
using WebMVC.Models.DTO.CartDTOFolder;
using WebMVC.Models.DTO.ProductDTOFolder;
using WebMVC.Service.IService;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;
        private ICartService _cartService;
        public HomeController(ILogger<HomeController> logger,IProductService productService,ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
           ApiResponse  response = await _productService.GetAllProductAsync();
            if (response != null && response.IsSuccess) { 
                IEnumerable<ProductDto> list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                return View(list);
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> DetailProductPage(int productId)
        {
            ProductDto productDto = new();
            ApiResponse response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto product= JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            else
            {
                TempData["error"] = response.ErrorMessage;
            }
            return View(productDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DetailProductPage(ProductDto productDto)
        {

            CartDto cartDto = new CartDto()
            {
                CartHeaderDto = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault().Value
                }
            };
            CartDetailsDto cartDetailsDto = new()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,
            };
            List<CartDetailsDto> listCartDetailDto = new List<CartDetailsDto>() { cartDetailsDto};
            cartDto.CartDetailsDto = listCartDetailDto;
            ApiResponse response = await _cartService.CartUpsertAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item is add in your Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response.ErrorMessage;
            }
            return View(productDto);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
