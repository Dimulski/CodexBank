using DemoShop.Models;
using System;

namespace DemoShop.Web.Models
{
    public class OrderDetailsViewModel
    {
        public string Id { get; set; }

        public string ProductName { get; set; }

        public string ProductImageUrl { get; set; }

        public decimal ProductPrice { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
