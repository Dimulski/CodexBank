using System;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Services.Models.Order
{
    public class OrderCreateServiceModel
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}