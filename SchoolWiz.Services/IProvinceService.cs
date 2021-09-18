using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IProvinceService
    {
        IEnumerable<Province> GetAll(bool includeInactive);
        IEnumerable<Province> GetProvincesForCountry(Guid countryId);
        Province GetById(Guid id);
        Province GetByName(string name, Guid countryId);
        Task Create(Province province);
        Task Edit(Province province);
        Task Delete(Guid id);
    }
}
