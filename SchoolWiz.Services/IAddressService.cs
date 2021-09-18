using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IAddressService
    {
        IEnumerable<Address> GetAll(bool includeInactive);
        Address GetById(Guid id);
        Address GetAddress(string unitNumber, string complexName, string streetAddress, string suburb, Guid cityId,
            string postalCode, Guid addressTypeId);
        Task CreateAsync(Address address);
        Task EditAsync(Address address);
        Task DeleteAsync(Address address);
    }
}
