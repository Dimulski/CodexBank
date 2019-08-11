using System.Collections.Generic;

namespace DemoShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductDetailsViewModel> Products { get; set; }
    }
}
