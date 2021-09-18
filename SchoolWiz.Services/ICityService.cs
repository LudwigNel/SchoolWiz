using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface ICityService
    {
        IEnumerable<City> GetAll(bool includeInActive);
        City GetById(Guid id);
        City GetByName(string name, Guid provinceId);
        Task CreateAsync(City city);
        Task EditAsync(City city);
        Task DeleteAsync(Guid id);
    }
}
