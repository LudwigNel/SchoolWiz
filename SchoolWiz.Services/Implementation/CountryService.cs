using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;

        public CountryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Countries.OrderBy(c => c.Name);
            return _context.Countries.Where(c => !c.IsDeleted).OrderBy(c => c.Name);
        }

        public Country GetById(Guid countryId) => _context.Countries.FirstOrDefault(c => c.Id == countryId);

        public Country GetByName(string name) => _context.Countries.FirstOrDefault(c => c.Name == name);

        public async Task CreateAsync(Country country)
        {
            await _context.Countries.AddAsync(country).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid countryId)
        {
            var country = GetById(countryId);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
