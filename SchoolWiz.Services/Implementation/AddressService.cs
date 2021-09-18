using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;

        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Address> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Addresses.OrderBy(a => a.UnitNumber).ThenBy(a => a.ComplexName)
                    .ThenBy(a => a.StreetAddress).ThenBy(a => a.Suburb).ThenBy(a => a.City.Name)
                    .ThenBy(a => a.City.Province.Name).ThenBy(a => a.City.Province.Country.Name);
            return _context.Addresses.Where(a => !a.IsDeleted).OrderBy(a => a.UnitNumber).ThenBy(a => a.ComplexName)
                .ThenBy(a => a.StreetAddress).ThenBy(a => a.Suburb).ThenBy(a => a.City.Name)
                .ThenBy(a => a.City.Province.Name).ThenBy(a => a.City.Province.Country.Name);
        }

        public Address GetById(Guid id) => _context.Addresses.FirstOrDefault(a => a.Id == id);

        public Address GetAddress(string unitNumber, string complexName, string streetAddress, string suburb,
            Guid cityId, string postalCode, Guid addressTypeIdGuid) => _context.Addresses.FirstOrDefault(a =>
            a.UnitNumber == unitNumber && a.ComplexName == complexName && a.StreetAddress == streetAddress &&
            a.Suburb == suburb && a.CityId == cityId && a.PostalCode == postalCode);

        public async Task CreateAsync(Address address)
        {
            await _context.Addresses.AddAsync(address).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
