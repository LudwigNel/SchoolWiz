using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Web.Models.User;

namespace SchoolWiz.Web.Controllers
{
    public class UserController : ApplicationBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
            : base(userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var users = _userManager.Users.Select(u => new UserIndexViewModel
                {
                    Id = u.Id,
                    FullName = $@"{u.FirstName.Trim()} {u.LastName.Trim()}",
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.MobileNumber,
                    IdentityNumber = u.IdentityNumber,
                    InActive = u.IsDeleted
                }).ToList().OrderBy(u => u.FullName);

                return View(users);

            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve the user list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                ViewBag.returnUrl = Request.Headers["Referer"].ToString();

                var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound();

                return View(new UserEditViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    IdentityNumber = user.IdentityNumber,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    MobileNumber = user.MobileNumber,
                    Email = user.Email,
                    IsDeleted = user.IsDeleted,
                    CreatedById = user.CreatedById,
                    CreatedBy = GetUserName(user.CreatedById),
                    CreatedDate = user.DateCreated,
                    ModifiedById = user.ModifiedById,
                    ModifiedBy = GetUserName(user.ModifiedById ?? Guid.Empty),
                    ModifiedDate = user.ModifiedDate,
                    InActive = user.IsDeleted,
                    Roles = string.Join(", ", await _userManager.GetRolesAsync(user))
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve the user for id {id} - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);
                    if (user == null)
                        return NotFound();

                    user.UserName = model.UserName;
                    user.IdentityNumber = model.IdentityNumber;
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    user.MobileNumber = model.MobileNumber;
                    user.Email = model.Email;
                    user.IsDeleted = model.InActive;
                    user.ModifiedById = model.ModifiedById;
                    user.ModifiedDate = model.ModifiedDate;

                    await _userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to edit the user for id {model.Id} - {ex.Message}");
            }

            return View();
        }
    }
}