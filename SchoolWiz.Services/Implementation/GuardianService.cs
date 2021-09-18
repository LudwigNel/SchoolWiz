using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class GuardianService : IGuardianService
    {
        private readonly ApplicationDbContext _context;

        public GuardianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StudentGuardian> GetStudentGuardians(Guid studentId) => _context.StudentGuardians
            .Include(g => g.Guardian).Include(sg => sg.Guardian.GuardianType).Where(sg => sg.StudentId == studentId);

        public IEnumerable<Guardian> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Guardians;
            return _context.Guardians.Where(g => !g.IsDeleted);
        }

        public Guardian GetById(Guid id) => _context.Guardians.Include(g => g.GuardianAddresses).Include("GuardianAddresses.Address").Include(g => g.StudentGuardians).FirstOrDefault(g => g.Id == id);

        public Guardian GetByIdentityNumber(string identityNumber) =>
            _context.Guardians.FirstOrDefault(g => g.IdentityNumber == identityNumber);

        public async Task CreateAsync(GuardianCreateViewModel guardian)
        {
            //Create guardian first
            var newGuardian = (await _context.Guardians.AddAsync(new Guardian
            {
                FirstName = guardian.FirstName,
                MiddleName = guardian.MiddleName,
                LastName = guardian.LastName,
                IdentityNumber = guardian.IdentityNumber,
                HomePhoneNumber = guardian.HomePhoneNumber,
                WorkPhoneNumber = guardian.WorkPhoneNumber,
                MobileNumber = guardian.MobileNumber,
                Email = guardian.Email,
                GuardianTypeId = guardian.GuardianTypeId,
                IsDeleted = guardian.Inactive,
                CreatedById = guardian.CreatedById,
                CreatedDate = guardian.CreatedDate
            }).ConfigureAwait(false)).Entity;

            //Physical address
            var physicalAddressTypeId = _context.AddressTypes
                .Single(at => at.Name == EnumExtensions.GetEnumDescription(AddressTypes.Physical)).Id;

            var physicalAddress = _context.Addresses.SingleOrDefault(a =>
                a.UnitNumber == guardian.AddressDetail.PhysicalAddress.UnitNumber &&
                a.ComplexName == guardian.AddressDetail.PhysicalAddress.ComplexName &&
                a.StreetAddress == guardian.AddressDetail.PhysicalAddress.StreetAddress &&
                a.Suburb == guardian.AddressDetail.PhysicalAddress.Suburb &&
                a.CityId == guardian.AddressDetail.PhysicalAddress.CityId &&
                a.PostalCode == guardian.AddressDetail.PhysicalAddress.PostalCode &&
                a.AddressTypeId == physicalAddressTypeId);

            //if yje physical address does not exist create it.
            if (physicalAddress == null)
                physicalAddress = (await _context.Addresses.AddAsync(new Address
                {
                    UnitNumber = guardian.AddressDetail.PhysicalAddress.UnitNumber,
                    ComplexName = guardian.AddressDetail.PhysicalAddress.ComplexName,
                    StreetAddress = guardian.AddressDetail.PhysicalAddress.StreetAddress,
                    Suburb = guardian.AddressDetail.PhysicalAddress.Suburb,
                    CityId = guardian.AddressDetail.PhysicalAddress.CityId,
                    PostalCode = guardian.AddressDetail.PhysicalAddress.PostalCode,
                    AddressTypeId = physicalAddressTypeId
                }).ConfigureAwait(false)).Entity;

            newGuardian.GuardianAddresses ??= new List<GuardianAddress>();
            newGuardian.GuardianAddresses.Add(new GuardianAddress
            {
                GuardianId = newGuardian.Id,
                AddressId = physicalAddress.Id,
                IsDeleted = false,
                CreatedById = guardian.CreatedById,
                CreatedDate = guardian.CreatedDate
            });

            //Postal address
            var postalAddressTypeId = _context.AddressTypes
                .Single(at => at.Name == EnumExtensions.GetEnumDescription(AddressTypes.Postal)).Id;

            var postalAddress = _context.Addresses.SingleOrDefault(a =>
                a.UnitNumber == guardian.AddressDetail.PostalAddress.UnitNumber &&
                a.ComplexName == guardian.AddressDetail.PostalAddress.ComplexName &&
                a.StreetAddress == guardian.AddressDetail.PostalAddress.StreetAddress &&
                a.Suburb == guardian.AddressDetail.PostalAddress.Suburb &&
                a.CityId == guardian.AddressDetail.PostalAddress.CityId &&
                a.PostalCode == guardian.AddressDetail.PostalAddress.PostalCode &&
                a.AddressTypeId == postalAddressTypeId);

            if (postalAddress == null)
                postalAddress = (await _context.Addresses.AddAsync(new Address
                {
                    UnitNumber = guardian.AddressDetail.PostalAddress.UnitNumber,
                    ComplexName = guardian.AddressDetail.PostalAddress.ComplexName,
                    StreetAddress = guardian.AddressDetail.PostalAddress.StreetAddress,
                    Suburb = guardian.AddressDetail.PostalAddress.Suburb,
                    CityId = guardian.AddressDetail.PostalAddress.CityId,
                    PostalCode = guardian.AddressDetail.PostalAddress.PostalCode,
                    AddressTypeId = postalAddressTypeId
                })).Entity;

            newGuardian.GuardianAddresses ??= new List<GuardianAddress>();
            newGuardian.GuardianAddresses.Add(new GuardianAddress
            {
                GuardianId = newGuardian.Id,
                AddressId = postalAddress.Id,
                IsDeleted = false,
                CreatedById = guardian.CreatedById,
                CreatedDate = guardian.CreatedDate
            });

            if (guardian.MainGuardian)
            {
                //remove previous primary contact.
                var studentGuardians = _context.StudentGuardians.Where(sg => guardian.Students.Contains(sg.StudentId));
                foreach (var studentGuardian in studentGuardians)
                    studentGuardian.PrimaryContact = false;
            }

            foreach (var studentId in guardian.Students)
            {
                newGuardian.StudentGuardians ??= new List<StudentGuardian>();
                newGuardian.StudentGuardians.Add(new StudentGuardian
                {
                    StudentId = studentId,
                    GuardianId = newGuardian.Id,
                    PrimaryContact = guardian.MainGuardian,
                    IsDeleted = false,
                    CreatedById = guardian.CreatedById,
                    CreatedDate = guardian.CreatedDate
                });
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(GuardianEditViewmodel guardian)
        {
            var existingGuardian = GetById(guardian.Id);

            //Update guardian
            existingGuardian.FirstName = guardian.FirstName;
            existingGuardian.MiddleName = guardian.MiddleName;
            existingGuardian.LastName = guardian.LastName;
            existingGuardian.IdentityNumber = guardian.IdentityNumber;
            existingGuardian.HomePhoneNumber = guardian.HomePhoneNumber;
            existingGuardian.WorkPhoneNumber = guardian.WorkPhoneNumber;
            existingGuardian.MobileNumber = guardian.MobileNumber;
            existingGuardian.Email = guardian.Email;
            existingGuardian.GuardianTypeId = guardian.GuardianTypeId;
            existingGuardian.IsDeleted = guardian.Inactive;
            existingGuardian.ModifiedById = guardian.ModifiedById;
            existingGuardian.ModifiedDate = guardian.ModifiedDate;

            //Physical address
            var physicalAddressTypeId = _context.AddressTypes
                .Single(at => at.Name == EnumExtensions.GetEnumDescription(AddressTypes.Physical)).Id;

            var physicalAddress = _context.Addresses.SingleOrDefault(a =>
                a.UnitNumber == guardian.PhysicalAddress.UnitNumber &&
                a.ComplexName == guardian.PhysicalAddress.ComplexName &&
                a.StreetAddress == guardian.PhysicalAddress.StreetAddress &&
                a.Suburb == guardian.PhysicalAddress.Suburb &&
                a.CityId == guardian.PhysicalAddress.CityId &&
                a.PostalCode == guardian.PhysicalAddress.PostalCode &&
                a.AddressTypeId == physicalAddressTypeId);

            if (physicalAddress == null)
                physicalAddress = (await _context.Addresses.AddAsync(new Address
                {
                    UnitNumber = guardian.PhysicalAddress.UnitNumber,
                    ComplexName = guardian.PhysicalAddress.ComplexName,
                    StreetAddress = guardian.PhysicalAddress.StreetAddress,
                    Suburb = guardian.PhysicalAddress.Suburb,
                    CityId = guardian.PhysicalAddress.CityId,
                    PostalCode = guardian.PhysicalAddress.PostalCode,
                    AddressTypeId = physicalAddressTypeId
                }).ConfigureAwait(false)).Entity;

            //remove existing guardian addresses
            var guardianAddressesToRemove = existingGuardian.GuardianAddresses.ToList();
            foreach (var guardianAddress in guardianAddressesToRemove)
                existingGuardian.GuardianAddresses.Remove(guardianAddress);

            existingGuardian.GuardianAddresses.Add(new GuardianAddress
            {
                GuardianId = existingGuardian.Id, 
                AddressId = physicalAddress.Id, 
                IsDeleted = false, 
                CreatedById = guardian.ModifiedById ?? Guid.Empty, 
                CreatedDate = guardian.ModifiedDate ?? DateTime.Now
            });

            //Postal address
            var postalAddressTypeId = _context.AddressTypes
                .Single(at => at.Name == EnumExtensions.GetEnumDescription(AddressTypes.Postal)).Id;

            var postalAddress = _context.Addresses.SingleOrDefault(a =>
                a.UnitNumber == guardian.PostalAddress.UnitNumber &&
                a.ComplexName == guardian.PostalAddress.ComplexName &&
                a.StreetAddress == guardian.PostalAddress.StreetAddress &&
                a.Suburb == guardian.PostalAddress.Suburb &&
                a.CityId == guardian.PostalAddress.CityId &&
                a.PostalCode == guardian.PostalAddress.PostalCode &&
                a.AddressTypeId == postalAddressTypeId);

            if (postalAddress == null)
                postalAddress = (await _context.Addresses.AddAsync(new Address
                {
                    UnitNumber = guardian.PostalAddress.UnitNumber,
                    ComplexName = guardian.PostalAddress.ComplexName,
                    StreetAddress = guardian.PostalAddress.StreetAddress,
                    Suburb = guardian.PostalAddress.Suburb,
                    CityId = guardian.PostalAddress.CityId,
                    PostalCode = guardian.PostalAddress.PostalCode,
                    AddressTypeId = postalAddressTypeId
                })).Entity;

            existingGuardian.GuardianAddresses.Add(new GuardianAddress
            {
                GuardianId = existingGuardian.Id,
                AddressId = postalAddress.Id,
                IsDeleted = false,
                CreatedById = guardian.ModifiedById ?? Guid.Empty,
                CreatedDate = guardian.ModifiedDate ?? DateTime.Now
            });

            if (guardian.MainGuardian)
            {
                //remove previous primary contact.
                var studentGuardians = _context.StudentGuardians.Where(sg => guardian.Students.Contains(sg.StudentId));
                foreach (var studentGuardian in studentGuardians)
                    studentGuardian.PrimaryContact = false;
            }

            var studentIds = guardian.Students;
            var studentGuardianToRemove =
                existingGuardian.StudentGuardians.Where(sg => !studentIds.Contains(sg.StudentId));
            foreach (var studentGuardian in studentGuardianToRemove)
                existingGuardian.StudentGuardians.Remove(studentGuardian);

            foreach (var studentId in guardian.Students)
            {
                var existingStudentGuardian = _context.StudentGuardians.SingleOrDefault(sg =>
                    sg.StudentId == studentId && sg.GuardianId == existingGuardian.Id);
                if (existingStudentGuardian != null)
                    existingStudentGuardian.PrimaryContact = guardian.MainGuardian;

                if (existingStudentGuardian == null)
                    existingGuardian.StudentGuardians.Add(new StudentGuardian
                    {
                        StudentId = studentId, 
                        GuardianId = existingGuardian.Id, 
                        PrimaryContact = guardian.MainGuardian,
                        IsDeleted = false,
                        CreatedById = guardian.ModifiedById ?? Guid.Empty,
                        CreatedDate = guardian.ModifiedDate ?? DateTime.Now
                    });
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);

        }

        public async Task DeleteAsync(Guid id)
        {
            var guardian = GetById(id);
            _context.Guardians.Remove(guardian);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
