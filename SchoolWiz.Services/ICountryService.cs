using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface ICountryService
    {
        IEnumerable<Country> GetAll(bool includeInactive);
        Country GetById(Guid countryId);
        Country GetByName(string name);
        Task CreateAsync(Country country);
        Task EditAsync(Country country);
        Task DeleteAsync(Guid countryId);
    }
}
