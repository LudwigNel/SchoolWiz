using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.AccountRate;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IAccountRateService
    {
        IEnumerable<AccountAccountRateViewModel> GetAllForAccount(Guid accountId);
        IEnumerable<Guid> GetAccountRateRateIds(Guid accountId);
        AccountRate GetById(Guid id);
        Task SaveAsync(AccountEditAccountRateViewModel rates);
        Task EditAsync(AccountRateEditViewModel accountRate);
    }
}
