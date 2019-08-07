using System.ComponentModel.DataAnnotations;

namespace CodexBank.Common.EmailSender
{
    public class SendGridConfiguration
    {
        [Required]
        public string ApiKey { get; set; }
    }
}
