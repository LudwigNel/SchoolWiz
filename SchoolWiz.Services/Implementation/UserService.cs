using System;
using System.Linq;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUserById(Guid userId) =>
            _context.ApplicationUsers.FirstOrDefault(user => user.Id == userId);
    }
}
