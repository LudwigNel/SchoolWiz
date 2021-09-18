using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWiz.Entity;

namespace SchoolWiz.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return string.Empty;
            var user = _userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));
            if (user == null) return string.Empty;
            return $@"{user.FirstName.Trim()} {user.LastName.Trim()}";
        }
    }
}