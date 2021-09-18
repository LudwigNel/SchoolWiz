using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IGuardianService
    {
        IEnumerable<StudentGuardian> GetStudentGuardians(Guid studentId);
        IEnumerable<Guardian> GetAll(bool includeInactive);
        Guardian GetById(Guid id);
        Guardian GetByIdentityNumber(string identityNumber);
        Task CreateAsync(GuardianCreateViewModel guardian);
        Task EditAsync(GuardianEditViewmodel guardian);
        Task DeleteAsync(Guid id);
    }
}
