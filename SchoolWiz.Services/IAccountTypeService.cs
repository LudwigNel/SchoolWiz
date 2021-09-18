using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IAccountTypeService
    {
        IEnumerable<AccountType> GetAll(bool includeInactive);
        AccountType GetById(Guid id);
        AccountType GetByName(string name);
        Task CreateAsync(AccountType accountType);
        Task EditAsync(AccountType accountType);
        Task DeleteAsync(Guid id);
    }
}
