using System;
using System.Linq;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class StudentGuardianService : IStudentGuardianService
    {
        private readonly ApplicationDbContext _context;

        public StudentGuardianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public StudentGuardian GetStudentGuardian(Guid studentId, Guid guardianId) =>
            _context.StudentGuardians.FirstOrDefault(sg => sg.StudentId == studentId && sg.GuardianId == guardianId);
    }
}
