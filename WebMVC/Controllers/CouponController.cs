using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.Models;
using WebMVC.Models.DTO.CouponDTOFolder;
using WebMVC.Service.IService;

namespace WebMVC.Controllers
{
    public class CouponController : Controller
    {
        private ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? list = new();
            ApiResponse response = await _couponService.GetAllCouponAsync();

            if (response != null && response.IsSuccess) {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
                return View(list);
            }
            TempData["error"] = response.ErrorMessage;
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> CreateCoupon()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateRequestCouponDTO requestCouponDTO)
        {
            if (ModelState.IsValid)
            {
                ApiResponse response = await _couponService.CreateCouponAsync(requestCouponDTO);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            
            return View(requestCouponDTO);
        }
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            ApiResponse apiResponse = await _couponService.GetCouponByIdAsync(couponId);
            if (apiResponse != null && apiResponse.IsSuccess == true) {
                CouponDTO couponDTO = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(apiResponse.Result));
                return View(couponDTO);
            }
            return NotFound();
        }
        [HttpPost]
		public async Task<IActionResult> DeleteCoupon(CouponDTO couponDTO)
		{
			ApiResponse apiResponse = await _couponService.DeleteCouponAsync(couponDTO.CouponId);
			if (apiResponse != null && apiResponse.IsSuccess == true)
			{
				
				return RedirectToAction(nameof(CouponIndex));
			}
			return View(couponDTO);
		}
	}
}
