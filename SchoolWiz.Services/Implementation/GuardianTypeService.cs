using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class GuardianTypeService : IGuardianTypeService
    {
        private readonly ApplicationDbContext _context;

        public GuardianTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GuardianType> GetAll(bool includeInactive)
        {
            if (includeInactive)
                return _context.GuardianTypes.OrderBy(gt => gt.Name);
            return _context.GuardianTypes.Where(gt => !gt.IsDeleted).OrderBy(gt => gt.Name);
        }

        public GuardianType GetById(Guid id) => _context.GuardianTypes.FirstOrDefault(gt => gt.Id == id);

        public GuardianType GetByName(string name) => _context.GuardianTypes.FirstOrDefault(gt => gt.Name == name);

        public async Task CreateAsync(GuardianType guardianType)
        {
            await _context.GuardianTypes.AddAsync(guardianType).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(GuardianType guardianType)
        {
            _context.GuardianTypes.Update(guardianType);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var guardianType = GetById(id);
            _context.GuardianTypes.Remove(guardianType);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
