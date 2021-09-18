using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IAccountStatusService
    {
        IEnumerable<AccountStatus> GetAll(bool includeInactive);
        AccountStatus GetById(Guid id);
        AccountStatus GetByName(string name);
        Task CreateAsync(AccountStatus accountStatus);
        Task EditAsync(AccountStatus accountStatus);
        Task DeleteAsync(Guid id);
    }
}
