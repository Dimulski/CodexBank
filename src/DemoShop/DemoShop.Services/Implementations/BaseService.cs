using DemoShop.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Services.Implementations
{
    public abstract class BaseService
    {
        protected readonly DemoShopDbContext context;

        protected BaseService(DemoShopDbContext context)
        {
            this.context = context;
        }

        public static bool IsEntityStateValid(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults,
                true);
        }
    }
}
