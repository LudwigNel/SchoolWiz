using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Extensions;
using SchoolWiz.WebApp.Models.AccountStatus;
using AccountStatus = SchoolWiz.Entity.AccountStatus;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountStatusController : ApplicationBaseController
    {
        private readonly IAccountStatusService _accountStatusService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AccountStatusController(IAccountStatusService accountStatusService, UserManager<ApplicationUser> userManager, ILoggerFactory factory)
            : base(userManager)
        {
            _accountStatusService = accountStatusService;
            _userManager = userManager;
            _logger = factory.CreateLogger("AccountStatus");
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var accountStatusList = _accountStatusService.GetAll(true).Select(status =>
                    new AccountStatusIndexViewModel
                    {
                        Id = status.Id,
                        Name = status.Name,
                        InActive = status.IsDeleted
                    });

                return View(accountStatusList);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account status list.");
                _logger.LogError($@"An error occurred while trying to retrieve the account status list - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AccountStatusCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountStatusCreateViewModel model)
        {
            try
            {
                var existing = _accountStatusService.GetByName(model.Name);
                if (existing != null)
                    ModelState.AddModelError(nameof(model.Name), "Account status already exists");

                if (ModelState.IsValid)
                {
                    var accountStatus = new SchoolWiz.Entity.AccountStatus
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _accountStatusService.CreateAsync(accountStatus);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Error", "Account Status successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the account status.");
                _logger.LogError($@"An error occurred while trying to create the account status - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var accountStatus = _accountStatusService.GetById(id);
                if (accountStatus == null)
                    return NotFound();

                return View(new AccountStatusEditViewModel
                {
                    Id = accountStatus.Id,
                    Name = accountStatus.Name,
                    Inactive = accountStatus.IsDeleted,
                    CreatedById = accountStatus.CreatedById,
                    CreatedBy = GetUserName(accountStatus.CreatedById),
                    CreatedDate = accountStatus.CreatedDate,
                    ModifiedById = accountStatus.ModifiedById,
                    ModifiedBy = GetUserName(accountStatus.ModifiedById ?? Guid.Empty),
                    ModifiedDate = accountStatus.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account status.");
                _logger.LogError($@"An error occurred while trying to retrieve the account status for id (id) - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountStatusEditViewModel model)
        {
            try
            {
                var existing = _accountStatusService.GetByName(model.Name);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Name), "Account status already exists");

                if (ModelState.IsValid)
                {
                    var accountStatus = _accountStatusService.GetById(model.Id);
                    if (accountStatus == null)
                        return NotFound();

                    accountStatus.Name = model.Name;
                    accountStatus.IsDeleted = model.Inactive;
                    accountStatus.ModifiedById = model.ModifiedById;
                    accountStatus.ModifiedDate = model.ModifiedDate;

                    await _accountStatusService.EditAsync(accountStatus);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success", "Account Status Successfully updated");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the account status.");
                _logger.LogError($@"An error occurred while trying to edit the account status for id (id) - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var accountStatus = _accountStatusService.GetById(id);
                if (accountStatus == null)
                    return NotFound();

                return PartialView("_Delete", new AccountStatusDeleteViewModel
                {
                    Id = accountStatus.Id,
                    Name = accountStatus.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account status.");
                _logger.LogError($@"An error occurred while trying to retrieve the account status for id (id) - {ex.Message}");
            }

            return PartialView("_Delete", new AccountStatusDeleteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AccountStatusDeleteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var accountStatus = _accountStatusService.GetById(model.Id);
                    if (accountStatus == null)
                        return NotFound();

                    await _accountStatusService.DeleteAsync(model.Id);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Account Status successfully deleted.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the account status.");
                _logger.LogError($@"An error occurred while trying to delete the account status for id (id) - {ex.Message}");
            }

            return PartialView("_Delete", model);
        }
    }
}