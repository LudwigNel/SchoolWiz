using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IVatService
    {
        IEnumerable<Vat> GetAll(bool includeInactive);
        Vat GetById(Guid id);
        Vat GetByValue(decimal value);
        Task CreateAsync(Vat vat);
        Task EditAsync(Vat vat);
        Task DeleteAsync(Guid id);
    }
}
