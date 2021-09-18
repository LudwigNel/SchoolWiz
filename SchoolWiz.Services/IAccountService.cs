using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.Account;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();

        Account GetById(Guid id);

        Task EditAsync(AccountEditViewModel account);
    }
}
