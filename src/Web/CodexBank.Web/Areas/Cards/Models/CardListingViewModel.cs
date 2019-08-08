using CodexBank.Web.Infrastructure.Collections;

namespace CodexBank.Web.Areas.Cards.Models
{
    public class CardListingViewModel
    {
        public PaginatedList<CardListingDto> Cards { get; set; }
    }
}
