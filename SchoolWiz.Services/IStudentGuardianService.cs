using System;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IStudentGuardianService
    {
        StudentGuardian GetStudentGuardian(Guid studentId, Guid guardianId);
    }
}
