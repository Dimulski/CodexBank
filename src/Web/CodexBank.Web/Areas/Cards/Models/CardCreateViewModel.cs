using CodexBank.Common.AutoMapping.Contracts;
using CodexBank.Services.Models.Card;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Web.Areas.Cards.Models
{
    public class CardCreateViewModel : IMapWith<CardCreateServiceModel>
    {
        public IEnumerable<SelectListItem> BankAccounts { get; set; }

        [Required]
        [Display(Name = "Choose account")]
        public string AccountId { get; set; }
    }
}
