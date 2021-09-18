using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.Role;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : ApplicationBaseController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public RoleController(RoleManager<ApplicationRole> roleManager, ILoggerFactory factory, IUserService userService, UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            _roleManager = roleManager;
            _logger = factory.CreateLogger("Tole");
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var roles = _roleManager.Roles.Select(r => new RoleIndexViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    InActive = r.IsDeleted
                });
                return View(roles);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the role list.");
                _logger.LogError($@"An error occurred while trying to load the user rol list. {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RoleCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userRole = new ApplicationRole
                    {
                        Name = model.Name,
                        IsDeleted = model.InActive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _roleManager.CreateAsync(userRole);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Role successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the role.");
                _logger.LogError($@"An error occurred while trying to create a new user role. {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
                if (role == null)
                    return NotFound();

                return View(new RoleEditViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    InActive = role.IsDeleted,
                    CreatedById = role.CreatedById,
                    CreatedBy = GetUserName(role.CreatedById),
                    CreatedDate = role.CreatedDate,
                    ModifiedBy = GetUserName(role.ModifiedById ?? Guid.Empty),
                    ModifiedById = role.ModifiedById,
                    ModifiedDate = role.ModifiedByDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the role.");
                _logger.LogError($@"An error occurred while trying to load the user role for role id - {id}. {ex.Message}");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = _roleManager.Roles.FirstOrDefault(r => r.Id == model.Id);
                    if (role == null)
                        return NotFound();

                    role.Name = model.Name;
                    role.IsDeleted = model.InActive;
                    role.ModifiedById = model.ModifiedById;
                    role.ModifiedByDate = model.ModifiedDate;

                    await _roleManager.UpdateAsync(role);

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "Role successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the role.");
                _logger.LogError($@"An error occurred while trying to edit the role for role id {model.Id} - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var role = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
                if (role == null)
                    return NotFound();
                return PartialView("_Delete", new RoleDeleteViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the role.");
                _logger.LogError($@"An error occurred while trying to display the role for role id {id} - {ex.Message}");
            }

            return PartialView("_Delete", new RoleDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleDeleteViewModel model)
        {
            try
            {
                var role = _roleManager.Roles.FirstOrDefault(r => r.Id == model.Id);
                if (role == null)
                    return NotFound();

                await _roleManager.DeleteAsync(role);

                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                    "Success", "Role successfully deleted.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the role.");
                _logger.LogError($@"An error occurred while trying to delete the role for role id {model.Id} - {ex.Message}");
            }

            return PartialView("_Delete", model);
        }

        [HttpGet]
        public async Task<IActionResult> Assign(Guid id)
        {
            try
            {

                ViewBag.returnUrl = Request.Headers["Referer"].ToString();
                var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    return NotFound();
                var model = CreateAssignRoleViewModel();
                model.UserName = $@"{user.FirstName.Trim()} {user.LastName.Trim()}";
                model.UserId = user.Id;
                var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
                model.SelectedRoles = _roleManager.Roles.Where(r => userRoles.Contains(r.Name)).Select(r => r.Id)
                    .ToArray();

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to load the assign role page.");
                _logger.LogError($@"An error occurred while trying to load the assign role page for user id {id} - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignRoleViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.returnUrl = returnUrl;
                    var roles = _roleManager.Roles.Where(r => model.SelectedRoles.Contains(r.Id)).Select(r => r.Name)
                        .ToList();
                    var user = _userManager.Users.FirstOrDefault(u => u.Id == model.UserId);
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                    if (roles.Any())
                        await _userManager.AddToRolesAsync(user, roles);
                    var selectedRoles = model.SelectedRoles;
                    model = CreateAssignRoleViewModel();
                    model.UserName = $@"{user?.FirstName.Trim()} {user?.LastName.Trim()}";
                    model.SelectedRoles = selectedRoles;

                    SetTempDataForToastNotification(true,
                        EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success",
                        "User roles successfully saved.");

                    if (!string.IsNullOrEmpty(returnUrl) && returnUrl.EndsWith("User/Edit") && user != null)
                        return RedirectToAction(nameof(Edit), nameof(User), new { id = user.Id });
                    return RedirectToAction(nameof(Index), nameof(User));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to assign the role.");
                _logger.LogError($@"An error occured while trying to assign user roles for user {model.UserName} - {ex.Message}");
            }

            return View(CreateAssignRoleViewModel());
        }

        private AssignRoleViewModel CreateAssignRoleViewModel()
        {
            var assignableRoles = _roleManager.Roles.ToList();
            var model = new AssignRoleViewModel
            {
                RoleList = new SelectList(assignableRoles, "Id", "Name")
            };
            return model;
        }
    }
}