using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class ProvinceService : IProvinceService
    {
        private readonly ApplicationDbContext _context;

        public ProvinceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Province> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Provinces.OrderBy(p => p.Name);
            return _context.Provinces.Where(p => !p.IsDeleted).OrderBy(p => p.Name);
        }

        public IEnumerable<Province> GetProvincesForCountry(Guid countryId) =>
            _context.Provinces.Where(p => p.CountryId == countryId && !p.IsDeleted);

        public Province GetById(Guid id) => _context.Provinces.FirstOrDefault(p => p.Id == id);

        public Province GetByName(string name, Guid countryId) => _context.Provinces.FirstOrDefault(p => p.Name == name && p.CountryId == countryId);

        public async Task Create(Province province)
        {
            await _context.Provinces.AddAsync(province).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Edit(Province province)
        {
            _context.Provinces.Update(province);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            var province = GetById(id);
            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
