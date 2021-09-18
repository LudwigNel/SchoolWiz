using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly ApplicationDbContext _context;

        public AccountTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountType> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.AccountTypes.OrderBy(accountType => accountType.Name);
            return _context.AccountTypes.Where(accountType => !accountType.IsDeleted)
                .OrderBy(accountType => accountType.Name);
        }

        public AccountType GetById(Guid id) =>
            _context.AccountTypes.FirstOrDefault(accountType => accountType.Id == id);

        public AccountType GetByName(string name) =>
            _context.AccountTypes.FirstOrDefault(accountType => accountType.Name == name);

        public async Task CreateAsync(AccountType accountType)
        {
            await _context.AccountTypes.AddAsync(accountType).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(AccountType accountType)
        {
            _context.AccountTypes.Update(accountType);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var accountType = GetById(id);
            _context.AccountTypes.Remove(accountType);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
