using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Common.Models.Account;
using SchoolWiz.Common.Models.AccountRate;
using SchoolWiz.Common.Models.Invoice;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Models.Account;

namespace SchoolWiz.WebApp.Controllers
{
    [Authorize(Roles = "Billing, Administrator")]
    public class AccountController : ApplicationBaseController
    {
        private readonly IAccountService _accountService;
        private readonly IAccountStatusService _accountStatusService;
        private readonly IAccountTypeService _accountTypeService;
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger _logger;

        public AccountController(UserManager<ApplicationUser> userManager, 
            IAccountService accountService, 
            ILoggerFactory loggerFactory, IAccountStatusService accountStatusService, IAccountTypeService accountTypeService, IInvoiceService invoiceService) 
            : base(userManager)
        {
            _logger = loggerFactory.CreateLogger("Account");
            _accountService = accountService;
            _accountStatusService = accountStatusService;
            _accountTypeService = accountTypeService;
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            try
            {
                var accounts = _accountService.GetAll().Select(a => new AccountIndexViewmodel
                {
                    Id = a.Id, 
                    AccountNumber = a.AccountNo, 
                    AccountName = $@"{a.Guardian.FirstName.Trim()} {a.Guardian.LastName.Trim()}".Trim(), 
                    CurrentPeriod = a.CurrentPeriod, 
                    Current = a.Current, 
                    ThirtyDays = a.ThirtyDays, 
                    SixtyDays = a.SixtyDays, 
                    NinetyDays = a.NinetyDays, 
                    HundredTwentyDays = a.HundredTwentyDays
                });

                return View(accounts);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account list.");
                _logger.LogError($@"An error occurred while trying to retrieve the account list - {ex.Message}");
            }

            return View(new List<AccountIndexViewmodel>());
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                var account = _accountService.GetById(id);
                if (account == null)
                    return NotFound();

                return View(new AccountEditViewModel
                {
                    Id = account.Id,
                    AccountName = $@"{account.Guardian.FirstName.Trim()} {account.Guardian.LastName.Trim()}".Trim(),
                    AccountNumber = account.AccountNo,
                    CurrentPeriod = account.CurrentPeriod,
                    Current = account.Current,
                    ThirtyDays = account.ThirtyDays,
                    SixtyDays = account.SixtyDays,
                    NinetyDays = account.NinetyDays,
                    HundredTwentyDays = account.HundredTwentyDays,
                    Total = account.Current + account.ThirtyDays + account.SixtyDays + account.NinetyDays +
                            account.HundredTwentyDays,
                    AccountStatusId = account.AccountStatusId,
                    AccountStatusList = new SelectList(_accountStatusService.GetAll(false), "Id", "Name"),
                    AccountTypeId = account.AccountTypeId,
                    AccountTypeList = new SelectList(_accountTypeService.GetAll(false), "Id", "Name"),
                    CreatedById = account.CreatedById,
                    CreatedBy = GetUserName(account.CreatedById),
                    CreatedDate = account.CreatedDate,
                    ModifiedById = account.ModifiedById,
                    ModifiedBy = GetUserName(account.ModifiedById ?? Guid.Empty), 
                    ModifiedDate = account.ModifiedDate,
                    AccountRates = new List<AccountRateDisplayViewmodel>(account.AccountRates.Select(r => new AccountRateDisplayViewmodel
                    {
                        Id = r.Id, 
                        Description = r.Rate.Description, 
                        Fee = r.Rate.Value.ToString("C"), 
                        DiscountPercentage = r.DiscountPercentage.ToString("C"), 
                        DiscountAmount = r.DiscountAmount.ToString("C"), 
                        InActive = r.IsDeleted
                    })), 
                    Invoices = new List<InvoiceDisplayViewModel>(_invoiceService.GetAccountInvoices(account.Id))
                });
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the account.");
                _logger.LogError($@"An error occurred while trying to retrieve the account - {ex.Message}");
            }

            return View(new AccountEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountEditViewModel model, string returnUrl)
        {
            try
            {
                ViewBag.returnUrl = returnUrl;

                if (ModelState.IsValid)
                {
                    var account = _accountService.GetById(model.Id);
                    if (account == null)
                        return NotFound();

                    account.AccountStatusId = model.AccountStatusId;
                    account.AccountTypeId = model.AccountTypeId;

                    await _accountService.EditAsync(model);

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(), "Success", "Account Successfully updated");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to edit the account.");
                _logger.LogError($@"An error occurred while trying to edit the account - {ex.Message}");
            }

            model.AccountStatusList = new SelectList(_accountStatusService.GetAll(false), "Id", "Name");
            model.AccountTypeList = new SelectList(_accountTypeService.GetAll(false), "Id", "Name");

            return View(model);
        }
    }
}
