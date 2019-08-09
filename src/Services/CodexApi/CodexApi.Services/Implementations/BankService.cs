﻿using AutoMapper.QueryableExtensions;
using CodexApi.Data;
using CodexApi.Services.Contracts;
using CodexApi.Services.Models.Bank;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodexApi.Services.Implementations
{
    public class BankService : BaseService, IBankService
    {
        public BankService(CodexApiDbContext context)
            : base(context)
        {
        }

        public async Task<T> GetBankAsync<T>(string bankName, string swiftCode, string bankCountry)
            where T : BankBaseServiceModel
        {
            var bank = await this.context
                .Banks
                .Where(b => string.Equals(b.Name, bankName, StringComparison.CurrentCultureIgnoreCase) &&
                            string.Equals(b.SwiftCode, swiftCode, StringComparison.CurrentCultureIgnoreCase)
                            && string.Equals(b.Location, bankCountry, StringComparison.CurrentCultureIgnoreCase))
                .ProjectTo<T>()
                .SingleOrDefaultAsync();

            return bank;
        }

        public async Task<IEnumerable<T>> GetAllBanksSupportingPaymentsAsync<T>()
            where T : BankBaseServiceModel
        {
            var banks = await this.context.Banks
                .Where(b => b.PaymentUrl != null)
                .OrderBy(b => b.Location)
                .ThenBy(b => b.Name)
                .ProjectTo<T>()
                .ToArrayAsync();

            return banks;
        }

        public async Task<T> GetBankByIdAsync<T>(string id)
            where T : BankBaseServiceModel
        {
            return await this.context.Banks
                .Where(b => b.Id == id)
                .ProjectTo<T>()
                .SingleOrDefaultAsync();
        }

        public async Task<T> GetBankByBankIdentificationCardNumbersAsync<T>(string identificationCardNumbers)
            where T : BankBaseServiceModel
            => await this.context
                .Banks
                .Where(b => b.BankIdentificationCardNumbers == identificationCardNumbers)
                .ProjectTo<T>()
                .SingleOrDefaultAsync();
    }
}
