using DemoShop.Services.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoShop.Services.Contracts
{
    public interface IProductsService
    {
        Task CreateAsync(ProductCreateServiceModel model);
        Task<IEnumerable<ProductDetailsServiceModel>> GetAllAsync();
    }
}
