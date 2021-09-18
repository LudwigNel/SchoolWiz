using System;
using SchoolWiz.Common.Models.Address;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Common.Models.Student;
using SchoolWiz.Entity;

namespace SchoolWiz.WebApp.Extensions
{
    public static class ViewModelExtensions
    {
        public static Student ConvertToStudent(this StudentCreateViewModel model, Guid createdById, DateTime createdDate)
        {
            return new Student
            {
                StudentNumber = model.StudentNumber,
                IdentityNumber = model.IdentityNumber,
                DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
                Email = model.Email,
                MedicalConditions = model.MedicalConditions,
                Age = model.Age,
                Doctor = model.DoctorName, 
                DoctorPhoneNumber = model.DoctorContactNumber, 
                UnderSupervisedMedication = model.UnderSupervisedMedication, 
                MedicationCausesDrowsiness = model.MedicationCausesDrowsiness,
                CreatedById = createdById,
                CreatedDate = createdDate, 
                IsDeleted = false
            };
        }

        public static Guardian ConvertToGuardian(this GuardianCreateViewModel model, Guid createdById, DateTime createdDate)
        {
            return new Guardian
            {
                IdentityNumber = model.IdentityNumber, 
                FirstName = model.FirstName, 
                MiddleName = model.MiddleName, 
                LastName = model.LastName, 
                HomePhoneNumber = model.HomePhoneNumber, 
                WorkPhoneNumber = model.WorkPhoneNumber, 
                MobileNumber = model.MobileNumber, 
                Email = model.Email,
                GuardianTypeId = model.GuardianTypeId,
                IsDeleted = false, 
                CreatedById = createdById, 
                CreatedDate = createdDate
            };
        }

        public static Guardian ConvertToGuardian(this AlternateGuardianCreateViewModel model, Guid createdById, DateTime createdDate)
        {
            return new Guardian
            {
                IdentityNumber = model.IdentityNumber,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                HomePhoneNumber = model.HomePhoneNumber,
                WorkPhoneNumber = model.WorkPhoneNumber,
                MobileNumber = model.MobileNumber,
                Email = model.Email,
                GuardianTypeId = model.GuardianTypeId,
                IsDeleted = false,
                CreatedById = createdById,
                CreatedDate = createdDate
            };
        }

        public static Address ConvertToAddress(this AddressCreateViewModel model, Guid createdById, DateTime createdDate)
        {
            return new Address
            {
                Id = Guid.Empty,
                UnitNumber = model.UnitNumber,
                ComplexName = model.ComplexName,
                StreetAddress = model.StreetAddress,
                Suburb = model.Suburb,
                CityId = model.CityId,
                PostalCode = model.PostalCode,
                AddressTypeId = model.AddressTypeId,
                CreatedById = createdById,
                CreatedDate = createdDate,
                IsDeleted = false
            };
        }
    }
}
