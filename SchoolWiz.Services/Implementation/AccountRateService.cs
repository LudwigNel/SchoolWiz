using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolWiz.Common.Models.AccountRate;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class AccountRateService : IAccountRateService
    {
        private readonly ApplicationDbContext _context;

        public AccountRateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EditAsync(AccountRateEditViewModel accountRate)
        {
            var accountRateToEdit = GetById(accountRate.Id);

            accountRateToEdit.DiscountPercentage = accountRate.DiscountPercentage;
            accountRateToEdit.DiscountAmount = accountRate.DiscountAmount;
            accountRateToEdit.IsDeleted = accountRate.Inactive;
            accountRateToEdit.ModifiedById = accountRate.ModifiedById;
            accountRateToEdit.ModifiedDate = accountRate.ModifiedDate;

            _context.AccountRates.Update(accountRateToEdit);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IEnumerable<Guid> GetAccountRateRateIds(Guid accountId) =>
            _context.AccountRates.Where(ar => ar.AccountId == accountId).Select(ar => ar.RateId);

        public IEnumerable<AccountAccountRateViewModel> GetAllForAccount(Guid accountId)
        {
            var rates = from rate in _context.Rates
                        join accountRate in _context.AccountRates on new { rateId = rate.Id, accountId = accountId } equals new
                        { rateId = accountRate.RateId, accountId = accountRate.AccountId }
                            into accountRates
                        from defaultAccountRates in accountRates.DefaultIfEmpty()
                        select new AccountAccountRateViewModel
                        {
                            Selected = defaultAccountRates.Id != Guid.Empty,
                            Id = rate.Id,
                            Description = rate.Description,
                            Value = rate.Value,
                            ValidFrom = rate.ValidFrom,
                            ValidTo = rate.ValidTo,
                            Inactive = rate.IsDeleted
                        };

            return rates;
        }

        public AccountRate GetById(Guid id) => _context.AccountRates.Include(ar => ar.Account).Include(ar => ar.Rate)
            .Include(ar => ar.Account.Guardian).SingleOrDefault(ar => ar.Id == id);

        public async Task SaveAsync(AccountEditAccountRateViewModel rates)
        {

            var ratesToDelete = _context.AccountRates.Where(ar => ar.AccountId == rates.AccountId);
            if (ratesToDelete.Any())
                foreach (var rateToDelete in ratesToDelete)
                    _context.AccountRates.Remove(rateToDelete);

            foreach (var rate in rates.RateIds)
            {
                await _context.AccountRates.AddAsync(new AccountRate
                {
                    AccountId = rates.AccountId,
                    RateId = rate,
                    IsDeleted = false,
                    CreatedById = rates.ModifiedById ?? Guid.Empty,
                    CreatedDate = rates.ModifiedDate ?? DateTime.Now
                }).ConfigureAwait(false);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
