using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.Rate;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class RateService : IRateService
    {
        private readonly ApplicationDbContext _context;

        public RateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rate> GetAll(bool includeInaActive)
        {
            if (includeInaActive)
                return _context.Rates.OrderBy(r => r.ValidFrom).ThenBy(r => r.ValidTo).ThenBy(r => r.Description);

            return _context.Rates.Where(r => !r.IsDeleted).OrderBy(r => r.ValidFrom).ThenBy(r => r.ValidTo).ThenBy(r => r.Description);
        }

        public Rate GetById(Guid id) => _context.Rates.SingleOrDefault(r => r.Id == id);

        public Rate GetRate(string description, DateTime? validFrom, DateTime? validTo) =>
            _context.Rates.SingleOrDefault(r =>
                r.Description == description && r.ValidFrom == validFrom && r.ValidTo == validTo);

        public async Task CreateAsync(RateCreateViewModel rate)
        {
            await _context.Rates.AddAsync(new Rate
            {
                Description = rate.Description,
                ValidFrom = rate.ValidFrom,
                ValidTo = rate.ValidTo,
                Value = rate.Value,
                IsDeleted = false,
                CreatedById = rate.CreatedById,
                CreatedDate = rate.CreatedDate
            }).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EditAsync(RateEditViewModel rate)
        {
            var existingRate = GetById(rate.Id);

            existingRate.Description = rate.Description;
            existingRate.ValidFrom = rate.ValidFrom;
            existingRate.ValidTo = rate.ValidTo;
            existingRate.Value = rate.Value;
            existingRate.IsDeleted = rate.Inactive;
            existingRate.ModifiedById = rate.ModifiedById;
            existingRate.ModifiedDate = rate.ModifiedDate;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
