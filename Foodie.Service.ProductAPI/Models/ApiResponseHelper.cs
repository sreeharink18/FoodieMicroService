using System.Net;

namespace Foodie.Service.ProductAPI.Models
{
    public static class ApiResponseHelper
    {
        public static ApiResponse ErrorResponse(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new ApiResponse
            {
                ErrorMessage = errorMessage,
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
