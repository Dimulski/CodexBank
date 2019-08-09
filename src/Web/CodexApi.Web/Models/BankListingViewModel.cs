using CodexApi.Services.Models.Bank;
using CodexBank.Common.AutoMapping.Contracts;

namespace CodexApi.Web.Models
{
    public class BankListingViewModel : IMapWith<BankListingServiceModel>
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string SwiftCode { get; set; }

        public string Id { get; set; }
    }
}
