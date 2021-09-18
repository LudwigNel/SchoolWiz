using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.Web.Models.Role;

namespace SchoolWiz.Web.Controllers
{
    //[Authorize(RoleList = "Administrator")]
    public class RoleController : ApplicationBaseController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleController> _logger;
        private readonly IUserService _userService;

        public RoleController(RoleManager<ApplicationRole> roleManager, ILogger<RoleController> logger, IUserService userService, UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
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
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
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
                return View(new RoleDeleteViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to display the role for role id {id} - {ex.Message}");
            }

            return View();
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

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to delete the role for role id {model.Id} - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Assign(Guid id)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            var model = CreateAssignRoleViewModel();
            model.UserName = $@"{user.FirstName.Trim()} {user.LastName.Trim()}";
            model.UserId = user.Id;
            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            model.SelectedRoles = _roleManager.Roles.Where(r => userRoles.Contains(r.Name)).Select(r => r.Id).ToArray();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.returnUrl = Request.Headers["Referer"].ToString();
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
                    return View(model);
                }
            }
            catch (Exception ex)
            {
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