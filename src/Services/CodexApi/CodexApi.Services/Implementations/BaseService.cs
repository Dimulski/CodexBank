using CodexApi.Data;

namespace CodexApi.Services.Implementations
{
    public abstract class BaseService
    {
        protected readonly CodexApiDbContext context;

        protected BaseService(CodexApiDbContext context)
        {
            this.context = context;
        }
    }
}
