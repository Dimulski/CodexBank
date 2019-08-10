﻿using System.ComponentModel.DataAnnotations;

namespace DemoShop.Services.Models.Product
{
    public class ProductCreateServiceModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000000000")]
        public decimal Price { get; set; }

        [Required]
        [Url]
        [MaxLength(300)]
        public string ImageUrl { get; set; }
    }
}
