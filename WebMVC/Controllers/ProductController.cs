using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.Models;
using WebMVC.Models.DTO.CouponDTOFolder;
using WebMVC.Models.DTO.ProductDTOFolder;
using WebMVC.Service;
using WebMVC.Service.IService;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
             ApiResponse response = await _productService.GetAllProductAsync();

            if (response != null && response.IsSuccess)  {
                IEnumerable<ProductDto> list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                return View(list);
            }
            TempData["error"] = response.ErrorMessage;
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }
        [HttpPost]

        
        public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
        {
            if (ModelState.IsValid)
            {
                ApiResponse response = await _productService.CreateProductAsync(productCreateDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = response.Result;
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response.ErrorMessage;
                }
            }
            return View(productCreateDto);
        }
        public async Task<IActionResult> UpdateProduct(int productId)
        {
            ApiResponse response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductUpdateDto product = JsonConvert.DeserializeObject<ProductUpdateDto>(Convert.ToString(response.Result));
                return View(product);
            }
            TempData["error"] = response?.ErrorMessage;
            return RedirectToAction(nameof(ProductIndex));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                ApiResponse response = await _productService.UpdateProductAsync(productUpdateDto,productUpdateDto.ProductId);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = response.Result;
                    return RedirectToAction(nameof(ProductIndex));

                }
                else
                {
                    TempData["error"] = response.ErrorMessage;
                }
            }
            return View(productUpdateDto);
        }
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            ApiResponse apiResponse = await _productService.GetProductByIdAsync(productId);
            if (apiResponse != null && apiResponse.IsSuccess == true)
            {
                ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(apiResponse.Result));
                return View(productDto);
            }
            TempData["error"] = "Something Went wrong...";
            return RedirectToAction(nameof(ProductIndex));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductDto productDto)
        {
            ApiResponse apiResponse = await _productService.DeleteProductAsync(productDto.ProductId);
            if (apiResponse != null && apiResponse.IsSuccess == true)
            {
                TempData["success"] = apiResponse.Result;
                return RedirectToAction(nameof(ProductIndex));
            }
            TempData["error"] = "Something Went wrong...";
            return RedirectToAction(nameof(ProductIndex));
        }
    }
}
