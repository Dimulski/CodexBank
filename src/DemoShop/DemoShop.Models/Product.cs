﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000000000")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
