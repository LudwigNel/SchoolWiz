using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Common.Models;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Students;
            return _context.Students.Where(s => !s.IsDeleted);
        }

        public Student GetById(Guid id) => _context.Students.FirstOrDefault(s => s.Id == id);

        public Student GetByName(string name) => _context.Students.FirstOrDefault(s => $@"{s.FirstName.Trim()} {s.MiddleName.Trim()} {s.LastName.Trim()}" == name);

        public async Task CreateAsync(Student student)
        {
            await _context.Students.AddAsync(student).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var student = GetById(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IEnumerable<Lookup> GetAllLookup() => _context.Students
            .Where(s => !s.IsDeleted).Select(s => new Lookup
            {
                Id = s.Id,
                Name = $@"{s.FirstName.Trim()} {s.LastName.Trim()}"
            });
    }
}
