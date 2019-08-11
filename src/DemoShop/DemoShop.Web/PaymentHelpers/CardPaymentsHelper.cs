using DemoShop.Web.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoShop.Web.PaymentHelpers
{
    public static class CardPaymentsHelper
    {
        public static async Task<bool> ProcessPaymentAsync(CardPaymentSubmitModel model, string codexApiSubmitUrl)
        {
            var client = new HttpClient();
            var request = await client.PostAsJsonAsync(codexApiSubmitUrl, model);

            return request.StatusCode == HttpStatusCode.OK;
        }
    }
}
