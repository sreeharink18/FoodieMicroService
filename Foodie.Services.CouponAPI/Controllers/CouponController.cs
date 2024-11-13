using AutoMapper;
using Foodie.Services.CouponAPI.Data;
using Foodie.Services.CouponAPI.Models;
using Foodie.Services.CouponAPI.Models.DTO.InComing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Foodie.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;
        public CouponController(ApplicationDbContext db,IMapper mapper) { 
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("getAllCoupons")]
        public async Task<IActionResult> GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon> couponsFromDb = _db.Coupons.ToList();
                IEnumerable<Coupon> couponDto = _mapper.Map<IEnumerable<Coupon>>(couponsFromDb);
                return Ok(ApiResponseHelper.SuccessResponse(couponDto));
            }
            catch (Exception ex) {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message,HttpStatusCode.InternalServerError));
            }
        }
        [HttpGet("getCouponById/{couponId:int}")]
        public async Task<IActionResult> GetCouponById(int couponId)
        {
            try
            {
                Coupon couponsFromDb =  _db.Coupons.FirstOrDefault(u=>u.CouponId == couponId);
                if (couponsFromDb == null) { 
                    return NotFound(ApiResponseHelper.ErrorResponse("Coupon Detail is not found",HttpStatusCode.NotFound));
                }
                Coupon couponDto = _mapper.Map<Coupon>(couponsFromDb);
                return Ok(ApiResponseHelper.SuccessResponse(couponDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpGet("getCouponByCouponCode/{couponCode}")]
        public async Task<IActionResult> GetCouponByCouponCode(string couponCode)
        {
            try
            {
                Coupon couponsFromDb = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == couponCode.ToLower());
                if (couponsFromDb == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Coupon Detail is not found", HttpStatusCode.NotFound));
                }
                Coupon couponDto = _mapper.Map<Coupon>(couponsFromDb);
                return Ok(ApiResponseHelper.SuccessResponse(couponDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpPost("createCoupon")]
        public async Task<IActionResult> CreateCoupon([FromBody]CreateRequestCouponDTO model)
        {
            try
            {
                Coupon couponDto = _mapper.Map<Coupon>(model);
                _db.Coupons.Add(couponDto);
                _db.SaveChanges();

                return Ok(ApiResponseHelper.SuccessResponse(couponDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpPut("updateCoupon/{couponId:int}")]
        public async Task<IActionResult> UpdateCoupon([FromBody] UpdateRequestCouponDTO model,int couponId)
        {
            try
            {
                Coupon couponsFromDb = _db.Coupons.FirstOrDefault(u => u.CouponId == couponId);
                if (couponsFromDb == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Coupon Detail is not found", HttpStatusCode.NotFound));
                }

                _mapper.Map(model, couponsFromDb);
                _db.SaveChanges();

                return Ok(ApiResponseHelper.SuccessResponse("Update Coupon Successfully completed"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpDelete("deleteCoupon/{couponId:int}")]
        public async Task<IActionResult> DeleteCoupon( int couponId)
        {
            try
            {
                Coupon couponsFromDb = _db.Coupons.FirstOrDefault(u => u.CouponId == couponId);
                if (couponsFromDb == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Coupon Detail is not found", HttpStatusCode.NotFound));
                }

                _db.Coupons.Remove(couponsFromDb);
                _db.SaveChanges();

                return Ok(ApiResponseHelper.SuccessResponse("Remove Coupon Successfully completed"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
    }
}
