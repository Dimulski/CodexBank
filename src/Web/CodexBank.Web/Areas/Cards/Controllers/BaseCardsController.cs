using CodexBank.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodexBank.Web.Areas.Cards.Controllers
{
    [Area("Cards")]
    [Route("[area]/[action]/{id?}")]
    public abstract class BaseCardsController : BaseController
    {
    }
}
