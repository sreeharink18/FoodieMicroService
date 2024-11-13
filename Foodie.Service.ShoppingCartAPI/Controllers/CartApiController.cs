using AutoMapper;
using Foodie.Service.ShoppingCartAPI.Data;
using Foodie.Service.ShoppingCartAPI.Model;
using Foodie.Service.ShoppingCartAPI.Model.DTO;
using Foodie.Service.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.PortableExecutable;

namespace Foodie.Service.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private IProductService _productService;
        public CartApiController(ApplicationDbContext db, IMapper mapper,IProductService productService)
        {
            _db = db;
            _mapper = mapper;
            _productService = productService;
        }
        [HttpGet("getCart/{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = new()
                {
                    CartHeaderDto = _mapper.Map<CartHeaderDto>(_db.CartHeaders
                    .FirstOrDefault(u => u.UserId == userId))
                };
                cartDto.CartDetailsDto = _mapper.Map<IEnumerable<CartDetailsDto>>(_db.CartDetails
                    .Where(u=>u.CartHeaderId == cartDto.CartHeaderDto.CartHeaderId));
                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();
                foreach (var item in cartDto.CartDetailsDto)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cartDto.CartHeaderDto.CartTotal += (item.Product.Price * item.Count);
                }
                return Ok(ApiResponseHelper.SuccessResponse(cartDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message.ToString()));
            }
        }

        [HttpPost("CartUpsert")]
        public async Task<IActionResult> CartUpsert([FromBody]CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = _db.CartHeaders.AsNoTracking().FirstOrDefault(u=>u.UserId == cartDto.CartHeaderDto.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeaderDto);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetailsDto.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetailsDto.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //check if details has same product
                    var cartDetailsFromDb = _db.CartDetails.AsNoTracking().FirstOrDefault(
                        u => u.ProductId == cartDto.CartDetailsDto.First().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        //Create cartDetails
                        cartDto.CartDetailsDto.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetailsDto.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //Update the quantities of Product
                        cartDto.CartDetailsDto.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetailsDto.First().CartDetailId = cartDetailsFromDb.CartDetailId;
                        cartDto.CartDetailsDto.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetailsDto.First()));
                        await _db.SaveChangesAsync();
                    }

                }
                return Ok(ApiResponseHelper.SuccessResponse(cartDto));
            }
            catch (Exception ex) {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message.ToString()));
            }
        }
        [HttpPost("removeCart")]
        public async Task<IActionResult> RemoveCart([FromBody]int cartDetailId)
        {
            try
            {
                CartDetails cartDetails = await _db.CartDetails.FirstOrDefaultAsync(u=>u.CartDetailId == cartDetailId);
                if (cartDetails == null) { 
                    return BadRequest(ApiResponseHelper.ErrorResponse($"Cart details is not found",HttpStatusCode.NotFound));
                }
                int totalCartItemCount =  _db.CartDetails.Where(u=>u.CartDetailId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCartItemCount == 1)
                {
                    var cartHeaderFromdb = _db.CartHeaders.FirstOrDefault(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderFromdb);
                    
                }
                await _db.SaveChangesAsync();
                return Ok(ApiResponseHelper.SuccessResponse("Remove Cart Detail Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message.ToString()));
            }
        }
        [HttpPost("EmailCartRequest")]
        public async Task<IActionResult> EmailCartRequest([FromBody]CartDto cartDto)
        {
            try
            {
                var cartDtoResponse =  cartDto;
                return Ok(ApiResponseHelper.SuccessResponse(cartDtoResponse));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message.ToString()));
            }
        }
    }
}
