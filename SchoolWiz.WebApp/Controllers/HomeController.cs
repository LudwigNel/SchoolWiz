using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWiz.Entity;
using SchoolWiz.WebApp.Models.Home;

namespace SchoolWiz.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public HomeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        : base(userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            var administratorRole = _roleManager.Roles.FirstOrDefault(r => r.Name == "Administrator");
            if (administratorRole == null)
                _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Administrator"
                }).GetAwaiter().GetResult();

            var userCount = _userManager.Users.Count();
            if (userCount == 1 && !User.IsInRole("Administrator"))
                _userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();

            return View(new HomeViewModel { UserName = GetUserName() });
        }
    }
}