using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;

        public CityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetAll(bool includeInActive)
        {
            if (includeInActive)
                return _context.Cities.Include(c => c.Province).Include(c => c.Province.Country).OrderBy(c => c.Name);
            return _context.Cities.Include(c => c.Province).Include(c => c.Province.Country)
                .Where(c => c.IsDeleted == false).OrderBy(c => c.Name);
        }

        public City GetById(Guid id) => _context.Cities.Include(c => c.Province).Include(c => c.Province.Country).FirstOrDefault(c => c.Id == id);

        public City GetByName(string name, Guid provinceId) => _context.Cities.FirstOrDefault(c => c.Name == name && c.ProvinceId == provinceId);

        public async Task CreateAsync(City city)
        {
            await _context.Cities.AddAsync(city).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async  Task DeleteAsync(Guid id)
        {
            var city = GetById(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
