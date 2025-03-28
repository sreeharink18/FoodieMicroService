﻿using System.ComponentModel.DataAnnotations;

namespace Foodie.Service.ProductAPI.Models.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string CategoryName { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
