using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RotativaCore;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Extensions;
using SchoolWiz.Entity;
using SchoolWiz.Services;
using SchoolWiz.WebApp.Models.Invoice;

namespace SchoolWiz.WebApp.Controllers
{
    public class InvoiceController : ApplicationBaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger _logger;

        public InvoiceController(UserManager<ApplicationUser> userManager, IInvoiceService invoiceService, ILoggerFactory loggerFactory)
            : base(userManager)
        {
            _logger = loggerFactory.CreateLogger("Invoice");
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currentPeriod = DateTime.Now.ToString("MMyyyy");

            try
            {
                var invoiceRun = _invoiceService.GetInvoiceRunForCurrentPeriod(currentPeriod);

                var model = new InvoiceIndexViewModel
                {
                    Period = currentPeriod,
                    Complete = invoiceRun?.Complete ?? false, 
                    CompletedBy = GetUserName(invoiceRun?.CompletedById ?? Guid.Empty),
                    CompletedDate = invoiceRun?.DateCompleted?.ToString("dd/MM/yyyy"),
                    Invoices = _invoiceService.GetInvoicesForCurrentPeriod(currentPeriod).Select(i => new InvoiceIndexDetailViewModel
                    {
                        Id = i.Id, 
                        InvoiceNumber = i.InvoiceNo, 
                        AccountNumber = i.Account.AccountNo, 
                        AccountName = $@"{i.Account.Guardian.FirstName.Trim()} {i.Account.Guardian.LastName.Trim()}".Trim(), 
                        ExcludingVatTotal = i.TotalExcludingVat.ToString("C"), 
                        VatAmount = i.TotalVat.ToString("C"), 
                        IncludingVatTotal = i.TotalIncludingVat.ToString("C"),
                    })
                };

                ViewBag.DisplayCompleteButton = !_invoiceService.InvoiceRunComplete(currentPeriod);

                return View(model);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the invoices for the current period.");
                _logger.LogError($@"An error occurred while trying to retrieve the invoices for the current period - {ex.Message}");
            }

            ViewBag.DisplayCompleteButton = !_invoiceService.InvoiceRunComplete(currentPeriod);
            return View(new InvoiceIndexViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateInvoices(InvoiceIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _invoiceService.GenerateInvoices(GetUserId());

                    SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                        "Success", "Invoice Run Complete.");

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to generate the invoices for the current period.");
                _logger.LogError($@"An error occurred while trying to generate the invoices for the current period - {ex.Message}");
            }

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteInvoiceRun(string period)
        {
            try
            {
                await _invoiceService.BulkCompleteInvoiceRun(period, GetUserId());

                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Success).ToLower(),
                    "Success", "Completed Invoice Run For Current Period.");
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to complete the invoice run for the current period.");
                _logger.LogError($@"An error occurred while trying to complete the invoice run for the current period - {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetInvoiceReport(Guid invoiceId)
        {
            try
            {
                var invoice = _invoiceService.GetInvoiceReport(invoiceId);
                return View("InvoiceReport", invoice);
            }
            catch (Exception ex)
            {
                SetTempDataForToastNotification(true, EnumExtensions.GetEnumDescription(ToastType.Error).ToLower(),
                    "Error", "Something went wrong while trying to retrieve the invoice report.");
                _logger.LogError($@"An error occurred while trying to retrieve the invoice report - {ex.Message}");
            }
            return View("InvoiceReport", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GenerateInvoicePdf(Guid invoiceId)
        {
            var invoicePdf = new ActionAsPdf("GetInvoiceReport", new {invoiceId = invoiceId})
            {
                FileName = "Invoice_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf"
            };
            return invoicePdf;
        }
    }
}
