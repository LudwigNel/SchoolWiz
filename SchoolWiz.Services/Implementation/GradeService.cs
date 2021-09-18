using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Grade> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Grades.OrderBy(g => g.Name);
            return _context.Grades.Where(g => !g.IsDeleted).OrderBy(g => g.Name);
        }

        public Grade GetById(Guid id) => _context.Grades.FirstOrDefault(g => g.Id == id);

        public Grade GetByName(string name) => _context.Grades.FirstOrDefault(g => g.Name == name);

        public async Task CreateAsync(Grade grade)
        {
            await _context.Grades.AddAsync(grade).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var grade = GetById(id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
