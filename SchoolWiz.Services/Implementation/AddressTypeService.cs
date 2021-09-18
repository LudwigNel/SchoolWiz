using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class AddressTypeService : IAddressTypeService
    {
        private readonly ApplicationDbContext _context;

        public AddressTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AddressType> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.AddressTypes.OrderBy(addressType => addressType.Name);
            return _context.AddressTypes.Where(accountType => !accountType.IsDeleted).OrderBy(addressType => addressType.Name);
        }

        public AddressType GetById(Guid id) =>
            _context.AddressTypes.FirstOrDefault(addressType => addressType.Id == id);

        public AddressType GetByName(string name) =>
            _context.AddressTypes.FirstOrDefault(addressType => addressType.Name == name);

        public async Task CreateAsync(AddressType addressType)
        {
            await _context.AddressTypes.AddAsync(addressType).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(AddressType addressType)
        {
            _context.AddressTypes.Update(addressType);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var addressType = GetById(id);
            _context.AddressTypes.Remove(addressType);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
