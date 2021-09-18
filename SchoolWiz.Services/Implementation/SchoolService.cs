using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class SchoolService : ISchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public School GetSchool() => _context.Schools.FirstOrDefault();

        public async Task CreateSchoolAsync(School school)
        {
            await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();
        }

        public async Task EditSchool(School school)
        {
            _context.Update(school);
            await _context.SaveChangesAsync();
        }

        public async Task Delete()
        {
            var school = GetSchool();
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
        }
    }
}
