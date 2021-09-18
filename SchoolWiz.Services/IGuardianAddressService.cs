using System;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IGuardianAddressService
    {
        GuardianAddress GetGuardianAddress(Guid guardianId, Guid addressId);
    }
}
