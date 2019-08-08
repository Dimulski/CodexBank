using CodexBank.Common;
using System.ComponentModel.DataAnnotations;

namespace CodexBank.Services.Models.Card
{
    using CodexBank.Models;

    public class CardCreateServiceModel : CardBaseServiceModel
    {
        public string Number { get; set; }

        [Required]
        [MaxLength(ModelConstants.Card.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ModelConstants.Card.ExpiryDateMaxLength, MinimumLength = ModelConstants.Card.ExpiryDateMaxLength)]
        public string ExpiryDate { get; set; }

        public string SecurityCode { get; set; }

        [Required]
        public string UserId { get; set; }

        public BankUser User { get; set; }

        [Required]
        public string AccountId { get; set; }

        public BankAccount Account { get; set; }
    }
}
