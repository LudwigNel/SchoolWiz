using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.Web.Models.AccountStatus;

namespace SchoolWiz.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountStatusController : ApplicationBaseController
    {
        private readonly IAccountStatusService _accountStatusService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountStatusController> _logger;

        public AccountStatusController(IAccountStatusService accountStatusService, UserManager<ApplicationUser> userManager, ILogger<AccountStatusController> logger) 
            : base(userManager)
        {
            _accountStatusService = accountStatusService;
            _userManager = userManager;
            _logger = logger;
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
                if (ModelState.IsValid)
                {
                    var accountStatus = new AccountStatus
                    {
                        Name = model.Name,
                        IsDeleted = model.Inactive,
                        CreatedById = model.CreatedById, 
                        CreatedDate = model.CreatedDate
                    };
                    await _accountStatusService.CreateAsync(accountStatus);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to create the account status - {ex.Message}");
            }

            return View();
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to edit the account status for id (id) - {ex.Message}");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var accountStatus = _accountStatusService.GetById(id);
                if (accountStatus == null)
                    return NotFound();

                return View(new AccountStatusDeleteViewModel
                {
                    Id = accountStatus.Id,
                    Name = accountStatus.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to retrieve the account status for id (id) - {ex.Message}");
            }

            return View();
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

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($@"An error occurred while trying to delete the account status for id (id) - {ex.Message}");
            }

            return View();
        }
    }
}