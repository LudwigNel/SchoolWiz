using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.AccountRate;
using SchoolWiz.Common.Models.Rate;
using SchoolWiz.Entity;
using SchoolWiz.Services;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Billing, Administrator")]
    public class AccountRateController : ApplicationBaseController
    {
        private readonly IAccountRateService _accountRateService;
        private readonly IAccountService _accountService;
        private readonly IRateService _rateService;
        private readonly ILogger _logger;

        public AccountRateController(UserManager<ApplicationUser> userManager, IAccountRateService accountRateService, ILoggerFactory loggerFactory, IAccountService accountService, IRateService rateService)
            : base(userManager)
        {
            _logger = loggerFactory.CreateLogger("AccountRate");
            _accountRateService = accountRateService;
            _accountService = accountService;
            _rateService = rateService;
        }

        [HttpGet]
        public IActionResult AddAccountRates(Guid accountId, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                var account = _accountService.GetById(accountId);
                if (account == null)
                    return NotFound();

                var model = new AccountEditAccountRateViewModel
                {
                    AccountId = accountId,
                    AccountNumber = account.AccountNo,
                    AccountName = $@"{account.Guardian.FirstName.Trim()} {account.Guardian.LastName.Trim()}".Trim(),
                    RateIds = _accountRateService.GetAccountRateRateIds(accountId).ToArray(),
                    Rates = new SelectList(_rateService.GetAll(false), "Id", "Description")
                };

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account rate detail.");
                _logger.LogError($@"An error occurred while trying to retrieve the account rate detail - {ex.Message}");
            }
            return View(new AccountEditAccountRateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccountRates(AccountEditAccountRateViewModel model, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                if (ModelState.IsValid)
                {
                    await _accountRateService.SaveAsync(model);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success", "Account Rates Successfully Saved");

                    return RedirectToAction("Edit", "Account", new { id = model.AccountId });
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the account rate detail.");
                _logger.LogError($@"An error occurred while trying to edit the account rate detail - {ex.Message}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                var accountRate = _accountRateService.GetById(id);
                if (accountRate == null)
                    return NotFound();

                var model = new AccountRateEditViewModel
                {
                    AccountId = accountRate.AccountId,
                    AccountNumber = accountRate.Account.AccountNo,
                    AccountName = $@"{accountRate.Account.Guardian.FirstName.Trim()} {accountRate.Account.Guardian.LastName.Trim()}".Trim(),
                    RateDescription = accountRate.Rate.Description,
                    Fee = accountRate.Rate.Value.ToString("C"),
                    DiscountPercentage = accountRate.DiscountPercentage,
                    DiscountAmount = accountRate.DiscountAmount,
                    Inactive = accountRate.IsDeleted,
                    CreatedById = accountRate.CreatedById,
                    CreatedBy = GetUserName(accountRate.CreatedById),
                    CreatedDate = accountRate.CreatedDate,
                    ModifiedById = accountRate.ModifiedById,
                    ModifiedBy = GetUserName(accountRate.ModifiedById ?? Guid.Empty),
                    ModifiedDate = accountRate.ModifiedDate
                };

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account rate.");
                _logger.LogError($@"An error occurred while trying to retrieve the account rate - {ex.Message}");
            }
            return View(new AccountRateEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountRateEditViewModel model, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                if (ModelState.IsValid)
                {
                    var accountRate = _accountRateService.GetById(model.Id);
                    if (accountRate == null)
                        return NotFound();

                    await _accountRateService.EditAsync(model);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success", "Account Rate Successfully Saved");

                    return RedirectToAction("Edit", "Account", new { id = model.AccountId });
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the account rate.");
                _logger.LogError($@"An error occurred while trying to edit the account rate - {ex.Message}");
            }

            return View(model);
        }
    }
}
