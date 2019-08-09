namespace CodexApi.Services.Models.Bank
{
    public class BankPaymentServiceModel : BankBaseServiceModel
    {
        public string ApiKey { get; set; }

        public string PaymentUrl { get; set; }

        public string CardPaymentUrl { get; set; }
    }
}
