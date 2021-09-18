using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class VatService : IVatService
    {
        private readonly ApplicationDbContext _context;

        public VatService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vat> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.Vat.OrderBy(v => v.CreatedDate).ThenBy(v => v.ValidFrom).ThenBy(v => v.ValidTo);
            return _context.Vat.Where(v => !v.IsDeleted).OrderBy(v => v.CreatedDate).ThenBy(v => v.ValidFrom).ThenBy(v => v.ValidTo);
        }

        public Vat GetById(Guid id) => _context.Vat.FirstOrDefault(v => v.Id == id);

        public Vat GetByValue(decimal value) =>
            _context.Vat.FirstOrDefault(v => v.Value == value);

        public async Task CreateAsync(Vat vat)
        {
            await _context.Vat.AddAsync(vat).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(Vat vat)
        {
            _context.Vat.Update(vat);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var vat = GetById(id);
            _context.Vat.Remove(vat);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
