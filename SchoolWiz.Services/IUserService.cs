using System;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IUserService
    {
        ApplicationUser GetUserById(Guid userId);
    }
}
