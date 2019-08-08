using AutoMapper;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.BankAccount;
using CodexBank.Services.Models.Card;
using CodexBank.Web.Areas.Cards.Models;
using CodexBank.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Web.Areas.Cards.Controllers
{
    public class CardsController : BaseCardsController
    {
        private const int CardsCountPerPage = 10;
        private readonly IBankAccountService bankAccountService;
        private readonly ICardService cardService;

        private readonly IUserService userService;

        public CardsController(
            IUserService userService,
            IBankAccountService bankAccountService,
            ICardService cardService)
        {
            this.userService = userService;
            this.bankAccountService = bankAccountService;
            this.cardService = cardService;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            pageIndex = Math.Max(1, pageIndex);

            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);
            var allCards = (await this.cardService
                    .GetCardsAsync<CardDetailsServiceModel>(userId, pageIndex, CardsCountPerPage))
                .Select(Mapper.Map<CardListingDto>)
                .ToPaginatedList(await this.cardService.GetCountOfAllCardsOwnedByUserAsync(userId), pageIndex, CardsCountPerPage);

            var cards = new CardListingViewModel
            {
                Cards = allCards
            };

            return this.View(cards);
        }

        public async Task<IActionResult> Create()
        {
            var userId = await this.userService.GetUserIdByUsernameAsync(this.User.Identity.Name);
            var userAccounts = await this.GetAllAccountsAsync(userId);

            var model = new CardCreateViewModel
            {
                BankAccounts = userAccounts
            };

            return this.View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetAllAccountsAsync(string userId)
            => (await this.bankAccountService
                    .GetAllAccountsByUserIdAsync<BankAccountIndexServiceModel>(userId))
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id })
                .ToArray();
    }
}
