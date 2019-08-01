using CodexBank.Common.Utils;
using CodexBank.Data;

namespace CodexBank.Services.Implementations
{
    public abstract class BaseService
    {
        protected readonly CodexBankDbContext context;

        protected BaseService(CodexBankDbContext context)
        {
            this.context = context;
        }

        protected bool IsEntityStateValid(object model) 
            => ValidationUtil.IsObjectValid(model);
    }
}
