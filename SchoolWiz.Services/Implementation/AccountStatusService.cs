using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class AccountStatusService : IAccountStatusService
    {
        private readonly ApplicationDbContext _context;

        public AccountStatusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountStatus> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.AccountStatuses.OrderBy(status => status.Name);
            return _context.AccountStatuses.Where(status => !status.IsDeleted).OrderBy(status => status.Name);
        }

        public AccountStatus GetById(Guid id) => _context.AccountStatuses.FirstOrDefault(status => status.Id == id);

        public AccountStatus GetByName(string name) =>
            _context.AccountStatuses.FirstOrDefault(status => status.Name == name);

        public async Task CreateAsync(AccountStatus accountStatus)
        {
            await _context.AccountStatuses.AddAsync(accountStatus).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(AccountStatus accountStatus)
        {
            _context.AccountStatuses.Update(accountStatus);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var accountStatus = GetById(id);
            _context.AccountStatuses.Remove(accountStatus);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
