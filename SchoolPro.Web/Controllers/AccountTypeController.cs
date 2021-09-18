using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;
using SchoolWiz.Services;
using SchoolWiz.Web.Models.AccountType;

namespace SchoolWiz.Web.Controllers
{
    public class AccountTypeController : ApplicationBaseController
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountStatusController> _logger;

        public AccountTypeController(IAccountTypeService accountTypeService, UserManager<ApplicationUser> userManager, ILogger<AccountStatusController> logger)
            : base(userManager)
        {
            _accountTypeService = accountTypeService;
            _userManager = userManager;
            _logger = logger;
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
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

                return View(new AccountTypeDeleteViewModel
                {
                    Id = accountType.Id,
                    Name = accountType.Name
                });
            }
            catch (Exception ex)
            {
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

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to delete the account type with id {model.Id} - {ex.Message}");
            }

            return View();
        }
    }
}