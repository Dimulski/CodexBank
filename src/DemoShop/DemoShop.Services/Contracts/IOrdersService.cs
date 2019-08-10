using DemoShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Services.Contracts
{
    public interface IOrdersService
    {
        Task<string> CreateAsync(OrderCreateServiceModel model);
        Task<OrderDetailsServiceModel> GetByIdAsync(string id);
        Task<IEnumerable<OrderDetailsServiceModel>> GetAllForUserAsync(string userName);
        Task SetPaymentStatus(string orderId, PaymentStatus paymentStatus);
    }
}
