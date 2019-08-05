using CodexBank.Common.AutoMapping.Contracts;
using CodexBank.Services.Models.BankAccount;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Web.Models.BankAccount
{
    public class BankAccountCreateBindingModel : IMapWith<BankAccountCreateServiceModel>
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
