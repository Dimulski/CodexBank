using System.ComponentModel.DataAnnotations;

namespace CodexBank.Common.Configuration
{
    class BankConfiguration
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}$")]
        public string UniqueIdentifier { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string CodexApiPublicKey { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{3}$")]
        public string First3CardDigits { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string CodexApiAddress { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
