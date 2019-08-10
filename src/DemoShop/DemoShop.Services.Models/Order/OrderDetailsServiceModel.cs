using DemoShop.Models;
using System;

namespace DemoShop.Services.Models.Order
{
    public class OrderDetailsServiceModel
    {
        public string Id { get; set; }

        public string ProductName { get; set; }

        public string ProductImageUrl { get; set; }

        public decimal ProductPrice { get; set; }

        public string UserName { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
