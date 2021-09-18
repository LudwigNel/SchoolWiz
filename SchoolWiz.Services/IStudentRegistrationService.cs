using System;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.Address;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IStudentRegistrationService
    {
        Task Register(Student student, Guardian mainGuardian, AddressCreateViewModel mainGuardianPostalAddress, AddressCreateViewModel mainGuardianPhyicalAddress, 
            Guardian alternateGuardian, AddressCreateViewModel alternateGuardianPostalAddress, AddressCreateViewModel alternateGuardianPhysicalAddress, 
            Guid gradeId, int schoolYear, Guid accountTypeId);
    }
}
