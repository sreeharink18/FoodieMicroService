using AutoMapper;
using Foodie.Service.ProductAPI.Data;
using Foodie.Service.ProductAPI.Models;
using Foodie.Service.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Foodie.Service.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]

    public class ProductApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductApiController(ApplicationDbContext db,IMapper mapper) { 
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> productList =  _db.Products.ToList();

                IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(productList);

                return Ok(ApiResponseHelper.SuccessResponse(productDtos));
            }
            catch (Exception ex) {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpGet("getProductById/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                Product product = _db.Products.FirstOrDefault(x=>x.ProductId == productId);
                if (product == null) {
                    return BadRequest(ApiResponseHelper.ErrorResponse("Product Details is not found..", HttpStatusCode.NotFound));
                }

                ProductDto productDto = _mapper.Map<ProductDto>(product);

                return Ok(ApiResponseHelper.SuccessResponse(productDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpPost("createProduct")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            try
            {
                if (ModelState.IsValid) { 
                    
                    Product productFromdb = _db.Products.FirstOrDefault(x=>x.Name.ToLower() == productCreateDto.Name.ToLower());
                    if (productFromdb != null) {
                        return BadRequest(ApiResponseHelper.ErrorResponse("This Product is already exists..."));
                    }

                    Product productdto = _mapper.Map<Product>(productCreateDto);

                    _db.Products.Add(productdto);
                    _db.SaveChanges();
                    return Ok(ApiResponseHelper.SuccessResponse("Create Product Successfully completed.."));
                }
                return BadRequest(ApiResponseHelper.ErrorResponse("Something went wrong..."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpPut("updateProduct/{productId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto model,int productId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Product productFromdb = _db.Products.FirstOrDefault(x => x.ProductId == productId);
                    if (productFromdb == null)
                    {
                        return BadRequest(ApiResponseHelper.ErrorResponse("This Product Detail is not found...",HttpStatusCode.NotFound));
                    }

                    _mapper.Map(model, productFromdb);

                    
                    _db.SaveChanges();
                    return Ok(ApiResponseHelper.SuccessResponse("Update Product Successfully completed.."));
                }
                return BadRequest(ApiResponseHelper.ErrorResponse("Something went wrong..."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
        [HttpDelete("deleteProduct/{productId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                Product product = _db.Products.FirstOrDefault(x => x.ProductId == productId);
                if (product == null)
                {
                    return BadRequest(ApiResponseHelper.ErrorResponse("Product Details is not found..", HttpStatusCode.NotFound));
                }

                _db.Products.Remove(product);
                _db.SaveChanges();
                return Ok(ApiResponseHelper.SuccessResponse("Remove Product Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
        }
    }
}
