﻿using System.Net;

namespace Foodie.Service.ShoppingCartAPI.Model
{
    public class ApiResponse
    {
        public object Result { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
