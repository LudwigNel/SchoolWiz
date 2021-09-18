using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Models.Account;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public AccountService(ILoggerFactory loggerFactory, ApplicationDbContext context)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("AccountService");
        }

        public IEnumerable<Account> GetAll() => _context.Accounts.Include(a => a.Guardian).OrderBy(a => a.AccountNo)
            .ThenBy(a => a.Guardian.LastName).ThenBy(a => a.Guardian.FirstName);

        public Account GetById(Guid id) =>
            _context.Accounts.Include(a => a.AccountRates).Include(a => a.AccountStatus).Include(a => a.Guardian)
                .Include(a => a.AccountRates).Include("AccountRates.Rate").Include(a => a.Invoices).FirstOrDefault(a => a.Id == id);

        public async Task EditAsync(AccountEditViewModel account)
        {
            var accountToUpdate = GetById(account.Id);
            accountToUpdate.AccountStatusId = account.AccountStatusId;
            accountToUpdate.AccountTypeId = accountToUpdate.AccountTypeId;
            accountToUpdate.ModifiedById = account.ModifiedById;
            accountToUpdate.ModifiedDate = account.ModifiedDate;
            
            _context.Accounts.Update(accountToUpdate);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
