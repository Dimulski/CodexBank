using System.ComponentModel.DataAnnotations;

namespace CodexApi.Web
{
    public class CodexApiConfiguration
    {
        [Required]
        public string Key { get; set; }
    }
}
