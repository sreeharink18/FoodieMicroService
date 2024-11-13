using System.Net;

namespace Foodie.Services.CouponAPI.Models
{
    public  class ApiResponse
    {
        public  object Result { get; set; }
        public  bool IsSuccess { get; set; }
        public  List<string> ErrorMessage { get; set; }
        public  HttpStatusCode StatusCode { get; set; }

       
    }
}
