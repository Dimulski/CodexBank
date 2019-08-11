using System.Linq;
using System.Threading.Tasks;
using DemoShop.Services.Contracts;
using DemoShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceProducts = await this.productsService.GetAllAsync();

            var viewProducts = serviceProducts.Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToArray();

            var homeViewModel = new HomeViewModel
            {
                Products = viewProducts
            };

            return this.View(homeViewModel);
        }
    }
}
