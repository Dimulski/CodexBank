using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DemoShop.Models
{
    public class DemoShopUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}
