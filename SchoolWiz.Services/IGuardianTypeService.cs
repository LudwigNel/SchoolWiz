using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IGuardianTypeService
    {
        IEnumerable<GuardianType> GetAll(bool includeInactive);
        GuardianType GetById(Guid id);
        GuardianType GetByName(string name);
        Task CreateAsync(GuardianType guardianType);
        Task EditAsync(GuardianType guardianType);
        Task DeleteAsync(Guid id);
    }
}
