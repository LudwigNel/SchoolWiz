using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.Rate;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IRateService
    {
        IEnumerable<Rate> GetAll(bool includeInaActive);
        Rate GetById(Guid id);
        Rate GetRate(string description, DateTime? validFrom, DateTime? validTo);
        Task CreateAsync(RateCreateViewModel rate);
        Task EditAsync(RateEditViewModel rate);
    }
}
