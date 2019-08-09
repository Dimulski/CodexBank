﻿using System.ComponentModel.DataAnnotations;

namespace CodexApi.Models
{
    public class Bank
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SwiftCode { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiAddress { get; set; }

        public string PaymentUrl { get; set; }

        public string CardPaymentUrl { get; set; }

        public string BankIdentificationCardNumbers { get; set; }
    }
}
