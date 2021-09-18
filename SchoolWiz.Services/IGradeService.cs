using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetAll(bool includeInactive);
        Grade GetById(Guid id);
        Grade GetByName(string name);
        Task CreateAsync(Grade grade);
        Task EditAsync(Grade grade);
        Task DeleteAsync(Guid id);
    }
}
