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
using SchoolWiz.WebApp.Models.AccountType;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountTypeController : ApplicationBaseController
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AccountTypeController(IAccountTypeService accountTypeService, UserManager<ApplicationUser> userManager, ILoggerFactory factory)
            : base(userManager)
        {
            _accountTypeService = accountTypeService;
            _userManager = userManager;
            _logger = factory.CreateLogger("AccountType");
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var accountTypes = _accountTypeService.GetAll(true).Select(accountType => new AccountTypeIndexViewModel
                {
                    Id = accountType.Id,
                    Name = accountType.Name,
                    InActive = accountType.IsDeleted
                });
                return View(accountTypes);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account type list.");
                _logger.LogError($@"An error occurred while trying to retrieve the account type list - {ex.Message}");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AccountTypeCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountTypeCreateViewModel model)
        {
            try
            {
                var existing = _accountTypeService.GetByName(model.Name);
                if(existing != null)
                    ModelState.AddModelError(nameof(model.Name), "Account type already exists.");

                if (ModelState.IsValid)
                {
                    var accountType = new AccountType
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById,
                        CreatedDate = model.CreatedDate
                    };
                    await _accountTypeService.CreateAsync(accountType);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                    "Success", "Account Type successfully created.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to create the account type.");
                _logger.LogError($@"An error occurred while trying to load the account type - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var accountType = _accountTypeService.GetById(id);
                if (accountType == null)
                    return NotFound();

                return View(new AccountTypeEditViewModel
                {
                    Id = accountType.Id,
                    Name = accountType.Name,
                    Inactive = accountType.IsDeleted,
                    CreatedById = accountType.CreatedById,
                    CreatedBy = GetUserName(accountType.CreatedById),
                    CreatedDate = accountType.CreatedDate,
                    ModifiedById = accountType.ModifiedById,
                    ModifiedBy = GetUserName(accountType.ModifiedById ?? Guid.Empty),
                    ModifiedDate = accountType.ModifiedDate
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account type.");
                _logger.LogError($@"An error occurred while trying to retrieve the account type with id {id} - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountTypeEditViewModel model)
        {
            try
            {
                var existing = _accountTypeService.GetByName(model.Name);
                if (existing != null && existing.Id != model.Id)
                    ModelState.AddModelError(nameof(model.Name), "Account type already exists.");

                if (ModelState.IsValid)
                {
                    var accountType = _accountTypeService.GetById(model.Id);
                    if (accountType == null)
                        return NotFound();

                    accountType.Name = model.Name;
                    accountType.IsDeleted = model.Inactive;
                    accountType.ModifiedById = model.ModifiedById;
                    accountType.ModifiedDate = model.ModifiedDate;

                    await _accountTypeService.EditAsync(accountType);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Account type successfully updated.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the account type.");
                _logger.LogError($@"An error occurred while trying to edit the account type with id {model.Id} - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var accountType = _accountTypeService.GetById(id);
                if (accountType == null)
                    return NotFound();

                return PartialView("_Delete", new AccountTypeDeleteViewModel
                {
                    Id = accountType.Id,
                    Name = accountType.Name
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account type.");
                _logger.LogError($@"An error occurred while trying to retrieve the account type with id {id} - {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AccountTypeDeleteViewModel model)
        {
            try
            {
                await _accountTypeService.DeleteAsync(model.Id);

                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                    "Success", "Account Type successfully deleted.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to delete the account type.");
                _logger.LogError($@"An error occurred while trying to delete the account type with id {model.Id} - {ex.Message}");
            }

            return View();
        }
    }
}