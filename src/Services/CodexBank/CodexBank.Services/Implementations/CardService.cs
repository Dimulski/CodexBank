using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodexBank.Data;
using CodexBank.Models;
using CodexBank.Services.Contracts;
using CodexBank.Services.Models.Card;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodexBank.Services.Implementations
{

    public class CardService : BaseService, ICardService
    {
        private readonly ICardHelper cardHelper;

        public CardService(CodexBankDbContext context, ICardHelper cardHelper)
            : base(context)
        {
            this.cardHelper = cardHelper;
        }

        public async Task<bool> CreateAsync(CardCreateServiceModel model)
        {
            if (!this.IsEntityStateValid(model) ||
                !this.context.Users.Any(u => u.Id == model.UserId))
            {
                return false;
            }

            string generatedNumber;
            string generated3DigitSecurityCode;
            do
            {
                generatedNumber = this.cardHelper.Generate16DigitNumber();
                generated3DigitSecurityCode = this.cardHelper.Generate3DigitSecurityCode();
            } while (await this.context.Cards.AnyAsync(a => a.Number == generatedNumber && a.SecurityCode == generated3DigitSecurityCode));

            var dbModel = Mapper.Map<Card>(model);
            dbModel.Number = generatedNumber;
            dbModel.SecurityCode = generated3DigitSecurityCode;

            await this.context.Cards.AddAsync(dbModel);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<T> GetAsync<T>(
            string cardNumber,
            string cardExpiryDate,
            string cardSecurityCode,
            string cardOwner)
            where T : CardBaseServiceModel
            => await this.context
                .Cards
                .Where(c =>
                    c.Name == cardOwner &&
                    c.Number == cardNumber &&
                    c.SecurityCode == cardSecurityCode &&
                    c.ExpiryDate == cardExpiryDate)
                .ProjectTo<T>()
                .SingleOrDefaultAsync();

        public async Task<T> GetAsync<T>(string id)
            where T : CardBaseServiceModel
            => await this.context.Cards
                .Where(c => c.Id == id)
                .ProjectTo<T>()
                .SingleOrDefaultAsync();

        public async Task<int> GetCountOfAllCardsOwnedByUserAsync(string userId)
            => await this.context
                .Cards
                .CountAsync(c => c.UserId == userId);

        public async Task<IEnumerable<T>> GetCardsAsync<T>(string userId, int pageIndex = 1, int count = int.MaxValue)
            where T : CardBaseServiceModel
            => await this.context
                .Cards
                .Where(c => c.UserId == userId)
                .Skip((pageIndex - 1) * count)
                .Take(count)
                .ProjectTo<T>()
                .ToArrayAsync();

        public async Task<bool> DeleteAsync(string id)
        {
            if (id == null)
            {
                return false;
            }

            var card = await this.context
                .Cards
                .Where(c => c.Id == id)
                .SingleOrDefaultAsync();

            if (card == null)
            {
                return false;
            }

            this.context.Cards.Remove(card);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}