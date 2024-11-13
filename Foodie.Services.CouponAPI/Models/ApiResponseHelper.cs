using System.Net;

namespace Foodie.Services.CouponAPI.Models
{
    public static class ApiResponseHelper
    {
        public static ApiResponse ErrorResponse(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new ApiResponse
            {
                ErrorMessage = new List<string> { errorMessage },
                IsSuccess = false,
                StatusCode = statusCode,
            };
        }
        public static ApiResponse SuccessResponse(object result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse
            {
                Result = result,
                IsSuccess = true,
                StatusCode = statusCode,
            };
        }
    }
}
