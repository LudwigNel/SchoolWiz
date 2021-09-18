using System;
using System.Linq;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class GuardianAddressService : IGuardianAddressService
    {
        private readonly ApplicationDbContext _context;

        public GuardianAddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public GuardianAddress GetGuardianAddress(Guid guardianId, Guid addressId) =>
            _context.GuardianAddresses.FirstOrDefault(ga => ga.GuardianId == guardianId && ga.AddressId == addressId);
    }
}
