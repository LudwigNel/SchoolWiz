using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IAddressTypeService
    {
        IEnumerable<AddressType> GetAll(bool includeInactive);
        AddressType GetById(Guid id);
        AddressType GetByName(string name);
        Task CreateAsync(AddressType addressType);
        Task EditAsync(AddressType addressType);
        Task DeleteAsync(Guid id);
    }
}
