using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Common.Models;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll(bool includeInactive);
        IEnumerable<Lookup> GetAllLookup();
        Student GetById(Guid id);
        Student GetByName(string name);
        Task CreateAsync(Student student);
        Task EditAsync(Student student);
        Task DeleteAsync(Guid id);
    }
}
