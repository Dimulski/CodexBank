using System.Collections.Generic;

namespace CodexBank.Web.Infrastructure.Middleware
{
    public class SecurityHeadersPolicy
    {
        public IDictionary<string, string> HeadersToSet { get; } = new Dictionary<string, string>();

        public ISet<string> HeadersToRemove { get; } = new HashSet<string>();
    }
}
