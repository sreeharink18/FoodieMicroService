﻿using static WebMVC.Utility.SD;

namespace WebMVC.Models
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } =ApiType.GET;
        public string Url { get; set; } 
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
