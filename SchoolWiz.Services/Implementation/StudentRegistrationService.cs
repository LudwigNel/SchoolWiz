using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Address;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;
using AccountStatus = SchoolWiz.Common.Enums.AccountStatus;

namespace SchoolWiz.Services.Implementation
{
    public class StudentRegistrationService : IStudentRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public StudentRegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Register(Student student, Guardian mainGuardian, AddressCreateViewModel mainGuardianPostalAddress, AddressCreateViewModel mainGuardianPhysicalAddress,
            Guardian alternateGuardian, AddressCreateViewModel alternateGuardianPostalAddress, AddressCreateViewModel alternateGuardianPhysicalAddress,
            Guid gradeId, int schoolYear, Guid accountTypeId)
        {
            var newStudent = (await _context.Students.AddAsync(student).ConfigureAwait(false)).Entity;
            var newMainGuardian = _context.Guardians.FirstOrDefault(g =>
                g.FirstName == mainGuardian.FirstName && g.MiddleName == mainGuardian.MiddleName &&
                g.LastName == mainGuardian.LastName && g.IdentityNumber == mainGuardian.IdentityNumber);
            if (newMainGuardian == null)
            {
                mainGuardian.Id = Guid.NewGuid();
                newMainGuardian = (await _context.Guardians.AddAsync(mainGuardian).ConfigureAwait(false)).Entity;

                //Check if the physical address exists
                var physicalAddress = _context.Addresses.SingleOrDefault(a =>
                    a.UnitNumber == mainGuardianPhysicalAddress.UnitNumber &&
                    a.ComplexName == mainGuardianPhysicalAddress.ComplexName &&
                    a.StreetAddress == mainGuardianPhysicalAddress.StreetAddress &&
                    a.Suburb == mainGuardianPhysicalAddress.Suburb &&
                    a.CityId == mainGuardianPhysicalAddress.CityId &&
                    a.PostalCode == mainGuardianPhysicalAddress.PostalCode &&
                    a.AddressTypeId == mainGuardianPhysicalAddress.AddressTypeId);

                //if the physical address does not exist create it
                if (physicalAddress == null)
                   physicalAddress = (await _context.Addresses.AddAsync(new Address
                    {
                        UnitNumber = mainGuardianPhysicalAddress.UnitNumber,
                        ComplexName = mainGuardianPhysicalAddress.ComplexName,
                        StreetAddress = mainGuardianPhysicalAddress.StreetAddress,
                        Suburb = mainGuardianPhysicalAddress.Suburb,
                        CityId = mainGuardianPhysicalAddress.CityId,
                        PostalCode = mainGuardianPhysicalAddress.PostalCode,
                        AddressTypeId = mainGuardianPhysicalAddress.AddressTypeId
                    }).ConfigureAwait(false)).Entity;

                mainGuardianPhysicalAddress.Id = physicalAddress.Id;
                await _context.GuardianAddresses.AddAsync(new GuardianAddress
                {
                    GuardianId = newMainGuardian.Id,
                    AddressId = physicalAddress.Id,
                    IsDeleted = false,
                    CreatedById = newMainGuardian.CreatedById,
                    CreatedDate = newMainGuardian.CreatedDate
                }).ConfigureAwait(false);

                //Check if the postal address exists
                var postalAddress = _context.Addresses.SingleOrDefault(a =>
                    a.UnitNumber == mainGuardianPostalAddress.UnitNumber &&
                    a.ComplexName == mainGuardianPostalAddress.ComplexName &&
                    a.StreetAddress == mainGuardianPostalAddress.StreetAddress &&
                    a.Suburb == mainGuardianPostalAddress.Suburb &&
                    a.CityId == mainGuardianPostalAddress.CityId &&
                    a.PostalCode == mainGuardianPostalAddress.PostalCode &&
                    a.AddressTypeId == mainGuardianPostalAddress.AddressTypeId);

                //If the postal address does not exist and its not he same as the physical address create it
                if(postalAddress == null)
                    postalAddress = (await _context.Addresses.AddAsync(new Address
                    {
                        UnitNumber = mainGuardianPostalAddress.UnitNumber,
                        ComplexName = mainGuardianPostalAddress.ComplexName,
                        StreetAddress = mainGuardianPostalAddress.StreetAddress,
                        Suburb = mainGuardianPostalAddress.Suburb,
                        CityId = mainGuardianPostalAddress.CityId,
                        PostalCode = mainGuardianPostalAddress.PostalCode,
                        AddressTypeId = mainGuardianPostalAddress.AddressTypeId
                    }).ConfigureAwait(false)).Entity;

                mainGuardianPostalAddress.Id = postalAddress?.Id ?? Guid.Empty;

                await _context.GuardianAddresses.AddAsync(new GuardianAddress
                {
                    GuardianId = newMainGuardian.Id,
                    AddressId = mainGuardianPostalAddress?.Id ?? Guid.Empty,
                    IsDeleted = false,
                    CreatedById = newMainGuardian.CreatedById,
                    CreatedDate = newMainGuardian.CreatedDate
                }).ConfigureAwait(false);

                await _context.StudentGuardians.AddAsync(new StudentGuardian
                {
                    CreatedById = student.CreatedById,
                    CreatedDate = student.CreatedDate,
                    GuardianId = newMainGuardian?.Id ?? Guid.Empty,
                    IsDeleted = false,
                    PrimaryContact = true,
                    StudentId = newStudent.Id
                }).ConfigureAwait(false);

            }
            else
            {
                var existingStudentGuardian = _context.StudentGuardians.SingleOrDefault(studentGuardian =>
                    studentGuardian.StudentId == student.Id && studentGuardian.GuardianId == newMainGuardian.Id);

                if (existingStudentGuardian == null)
                {
                    mainGuardian.StudentGuardians ??= new List<StudentGuardian>();
                    mainGuardian.StudentGuardians.Add(new StudentGuardian
                    {
                        GuardianId = newMainGuardian.Id,
                        StudentId = student.Id,
                        PrimaryContact = true,
                        IsDeleted = false,
                        CreatedById = student.CreatedById,
                        CreatedDate = student.CreatedDate
                    });
                }
            }

            if (alternateGuardian != null)
            {
                var newAlternateGuardian = _context.Guardians.FirstOrDefault(g =>
                    g.FirstName == alternateGuardian.FirstName && g.MiddleName == alternateGuardian.MiddleName &&
                    g.LastName == alternateGuardian.LastName && g.IdentityNumber == alternateGuardian.IdentityNumber);
                if (newAlternateGuardian == null)
                {
                    alternateGuardian.Id = Guid.NewGuid();
                    newAlternateGuardian = (await _context.Guardians.AddAsync(alternateGuardian).ConfigureAwait(false)).Entity;

                    //Check if the physical address exists
                    var physicalAddress = _context.Addresses.SingleOrDefault(a =>
                        a.UnitNumber == alternateGuardianPhysicalAddress.UnitNumber &&
                        a.ComplexName == alternateGuardianPhysicalAddress.ComplexName &&
                        a.StreetAddress == alternateGuardianPhysicalAddress.StreetAddress &&
                        a.Suburb == alternateGuardianPhysicalAddress.Suburb &&
                        a.CityId == alternateGuardianPhysicalAddress.CityId &&
                        a.PostalCode == alternateGuardianPhysicalAddress.PostalCode &&
                        a.AddressTypeId == alternateGuardianPhysicalAddress.AddressTypeId);

                    //if the physical address does not exist create it
                    if (physicalAddress == null && !mainGuardianPhysicalAddress.Equals(alternateGuardianPhysicalAddress))
                        physicalAddress = (await _context.Addresses.AddAsync(new Address
                        {
                            UnitNumber = alternateGuardianPhysicalAddress.UnitNumber,
                            ComplexName = alternateGuardianPhysicalAddress.ComplexName,
                            StreetAddress = alternateGuardianPhysicalAddress.StreetAddress,
                            Suburb = alternateGuardianPhysicalAddress.Suburb,
                            CityId = alternateGuardianPhysicalAddress.CityId,
                            PostalCode = alternateGuardianPhysicalAddress.PostalCode,
                            AddressTypeId = alternateGuardianPhysicalAddress.AddressTypeId
                        }).ConfigureAwait(false)).Entity;

                    var physicalAddressId = physicalAddress?.Id ?? Guid.Empty;
                    if (physicalAddress == null && mainGuardianPhysicalAddress.Equals(alternateGuardianPhysicalAddress))
                        physicalAddressId = mainGuardianPhysicalAddress.Id;

                    await _context.GuardianAddresses.AddAsync(new GuardianAddress
                    {
                        GuardianId = newAlternateGuardian.Id,
                        AddressId = physicalAddressId,
                        IsDeleted = false,
                        CreatedById = newMainGuardian.CreatedById,
                        CreatedDate = newMainGuardian.CreatedDate
                    }).ConfigureAwait(false);

                    //Check if the postal address exists
                    var postalAddress = _context.Addresses.SingleOrDefault(a =>
                        a.UnitNumber == alternateGuardianPostalAddress.UnitNumber &&
                        a.ComplexName == alternateGuardianPostalAddress.ComplexName &&
                        a.StreetAddress == alternateGuardianPostalAddress.StreetAddress &&
                        a.Suburb == alternateGuardianPostalAddress.Suburb &&
                        a.CityId == alternateGuardianPostalAddress.CityId &&
                        a.PostalCode == alternateGuardianPostalAddress.PostalCode &&
                        a.AddressTypeId == alternateGuardianPostalAddress.AddressTypeId);

                    //If the postal address does not exist and its not he same as the physical address create it
                    if (postalAddress == null && !alternateGuardianPhysicalAddress.EqualsWithoutType(alternateGuardianPostalAddress))
                        postalAddress = (await _context.Addresses.AddAsync(new Address
                        {
                            UnitNumber = alternateGuardianPostalAddress.UnitNumber,
                            ComplexName = alternateGuardianPostalAddress.ComplexName,
                            StreetAddress = alternateGuardianPostalAddress.StreetAddress,
                            Suburb = alternateGuardianPostalAddress.Suburb,
                            CityId = alternateGuardianPostalAddress.CityId,
                            PostalCode = alternateGuardianPostalAddress.PostalCode,
                            AddressTypeId = alternateGuardianPostalAddress.AddressTypeId
                        }).ConfigureAwait(false)).Entity;

                    var postalAddressId = postalAddress?.Id ?? Guid.Empty;
                    if (postalAddress == null &&
                        mainGuardianPostalAddress.Equals(alternateGuardianPostalAddress))
                        postalAddressId = mainGuardianPostalAddress.Id;

                    await _context.GuardianAddresses.AddAsync(new GuardianAddress
                    {
                        GuardianId = newAlternateGuardian.Id,
                        AddressId = postalAddressId,
                        IsDeleted = false,
                        CreatedById = newMainGuardian.CreatedById,
                        CreatedDate = newMainGuardian.CreatedDate
                    }).ConfigureAwait(false);

                    await _context.StudentGuardians.AddAsync(new StudentGuardian
                    {
                        CreatedById = student.CreatedById,
                        CreatedDate = student.CreatedDate,
                        GuardianId = newAlternateGuardian?.Id ?? Guid.Empty,
                        IsDeleted = false,
                        PrimaryContact = false,
                        StudentId = newStudent.Id
                    }).ConfigureAwait(false);
                }
                else
                {
                    var existingStudentGuardian = _context.StudentGuardians.SingleOrDefault(studentGuardian =>
                        studentGuardian.StudentId == student.Id && studentGuardian.GuardianId == newAlternateGuardian.Id);

                    if (existingStudentGuardian == null)
                    {
                        mainGuardian.StudentGuardians ??= new List<StudentGuardian>();
                        mainGuardian.StudentGuardians.Add(new StudentGuardian
                        {
                            GuardianId = newAlternateGuardian.Id,
                            StudentId = student.Id,
                            PrimaryContact = false,
                            IsDeleted = false,
                            CreatedById = student.CreatedById,
                            CreatedDate = student.CreatedDate
                        });
                    }
                }
            }

            await _context.StudentRegistrations.AddRangeAsync(new StudentRegistration
            {
                CreatedById = student.CreatedById,
                CreatedDate = DateTime.Now,
                GradeId = gradeId,
                IsDeleted = false,
                SchoolYear = schoolYear,
                StudentId = newStudent.Id
            });

            var accountStatusId = _context.AccountStatuses.FirstOrDefault(status =>
                status.Name == EnumExtensions.GetEnumDescription(AccountStatus.Active));

            var accountNumber = GenerateAccountNumber(mainGuardian.FirstName, mainGuardian.LastName);

            var existingAccount =
                _context.Accounts.FirstOrDefault(
                    a => a.AccountNo == accountNumber && a.GuardianId == newMainGuardian.Id);

            if (existingAccount == null)
                await _context.Accounts.AddAsync(new Account
                {
                    GuardianId = mainGuardian.Id,
                    AccountNo = accountNumber,
                    AccountStatusId = accountStatusId?.Id ?? Guid.Empty,
                    CurrentPeriod = DateTime.Now.ToString("MMyyyy"),
                    Current = 0,
                    ThirtyDays = 0,
                    SixtyDays = 0,
                    NinetyDays = 0,
                    HundredTwentyDays = 0,
                    AccountTypeId = accountTypeId,
                    CreatedById = student.CreatedById,
                    CreatedDate = student.CreatedDate,
                    IsDeleted = false
                });

            await _context.SaveChangesAsync();
        }

        private string GenerateAccountNumber(string firstName, string lastname)
        {
            string prefix;
            prefix = lastname.Length > 2 ? lastname.Substring(0, 3).ToUpper() : $@"{firstName.Substring(0, 1)} {lastname.Substring(0, 2)}".ToUpper();

            var lastAccountNumber = _context.Accounts.OrderByDescending(a => a.AccountNo)
                .FirstOrDefault(a => a.AccountNo.Contains(prefix));

            if (lastAccountNumber == null)
                return $@"{prefix.ToUpper()}001";

            var numberPart = lastAccountNumber.AccountNo.Substring(lastAccountNumber.AccountNo.Length - 3, 3);
            int.TryParse(numberPart, out var number);

            return prefix + number.ToString().PadLeft(2, '0');
        }
    }
}
