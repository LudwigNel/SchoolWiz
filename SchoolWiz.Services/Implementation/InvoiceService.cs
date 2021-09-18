using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolWiz.Common.Models.Account;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Common.Models.Invoice;
using SchoolWiz.Common.Models.School;
using SchoolWiz.Entity;
using SchoolWiz.Persistence;

namespace SchoolWiz.Services.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GenerateInvoices(Guid createdById)
        {
            var createdDate = DateTime.Now;

            var school = _context.Schools.Single(s => !s.IsDeleted);

            var newPeriod = DateTime.Now.ToString("MMyyyy");
            var previousPeriod = _context.InvoiceRuns.OrderByDescending(i => i.DateCompleted).FirstOrDefault()?.Period;

            if (previousPeriod == null)
                previousPeriod = DateTime.Now.AddMonths(-1).ToString("MMyyyy");

            int.TryParse(previousPeriod.Substring(0, 2), out var previousPeriodMonth);
            int.TryParse(previousPeriod.Substring(2, 4), out var previousPeriodYear);

            if (DateTime.Now.Month == previousPeriodMonth && DateTime.Now.Year == previousPeriodYear)
                return;

            var invoiceRun = (await _context.InvoiceRuns.AddAsync(new InvoiceRun
            {
                Period = newPeriod,
                Complete = false,
                CompletedById = null,
                DateCompleted = null,
                IsDeleted = false,
                CreatedById = createdById,
                CreatedDate = createdDate
            }).ConfigureAwait(false)).Entity;

            var activeAccounts = _context.AccountRates.Include(ar => ar.Account)
                .Include(ar => ar.Rate).Where(ar => !ar.IsDeleted && !ar.Account.IsDeleted).ToList();

            foreach (var activeAccount in activeAccounts)
            {
                var invoiceNumber = GetInvoiceNumber();

                var newInvoice = new Invoice
                {
                    AccountId = activeAccount.AccountId,
                    InvoiceNo = invoiceNumber,
                    Period = newPeriod,
                    SchoolId = school.Id,
                    IsDeleted = false,
                    CreatedById = createdById,
                    CreatedDate = createdDate, 
                    InvoiceRunId = invoiceRun.Id
                };

                newInvoice = (await _context.Invoices.AddAsync(newInvoice).ConfigureAwait(false)).Entity;

                await GenerateInvoiceItems(newInvoice, activeAccount.Account.GuardianId).ConfigureAwait(false);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Invoice GetById(Guid id) => _context.Invoices.Include(i => i.Account).Include(i => i.AccountRates)
            .SingleOrDefault(i => i.Id == id);

        private string GetInvoiceNumber()
        {
            var lastInvoice = _context.Invoices
                .OrderByDescending(i => i.CreatedDate).FirstOrDefault();

            var number = "INV000000";
            if (lastInvoice != null)
                number = lastInvoice.InvoiceNo.Substring(3, 6);

            if (number.Contains("INV"))
                number = number.Substring(3, 6);

            int.TryParse(number, out var numberWithoutPrefix);
            numberWithoutPrefix = numberWithoutPrefix + 1;
            var invoiceNumber = "INV" + (numberWithoutPrefix).ToString().PadLeft(6, '0');
            return invoiceNumber;
        }

        private async Task GenerateInvoiceItems(Invoice invoice, Guid guardianId)
        {
            var students = _context.StudentGuardians.Include(sg => sg.Student).Where(sg => sg.GuardianId == guardianId).ToList();
            var accountRates = _context.AccountRates.Include(ar => ar.Rate)
                .Where(ar => ar.AccountId == invoice.AccountId).ToList();
            var vat = _context.Vat.Single(v => !v.IsDeleted);
            decimal invoiceTotal = 0;
            decimal invoiceVatAmount = 0;
            decimal invoiceExcludingVatTotal = 0;

            foreach (var student in students)
            {
                foreach (var accountRate in accountRates)
                {
                    var vatAmount = (accountRate.Rate.Value * vat.Value / 100);
                    await _context.InvoiceItems.AddAsync(new InvoiceItem
                    {
                        InvoiceId = invoice.Id,
                        Description = $@"{student.Student.FirstName.Trim()} {student.Student.LastName.Trim()} - {accountRate.Rate.Description}",
                        ItemCharge = accountRate.Rate.Value,
                        VatAmount = vatAmount,
                        ItemTotal = accountRate.Rate.Value + vatAmount,
                        VatId = vat.Id,
                        IsDeleted = false,
                        CreatedById = invoice.CreatedById,
                        CreatedDate = invoice.CreatedDate
                    }).ConfigureAwait(false);

                    invoiceExcludingVatTotal += accountRate.Rate.Value;
                    invoiceVatAmount += vatAmount;
                    invoiceTotal += (accountRate.Rate.Value + vatAmount);
                }
            }

            invoice.TotalExcludingVat = invoiceExcludingVatTotal;
            invoice.TotalVat = invoiceVatAmount;
            invoice.TotalIncludingVat = invoiceTotal;
        }

        public IEnumerable<Invoice> GetInvoicesForCurrentPeriod(string period) => _context.Invoices
            .Include(i => i.InvoiceRun).Include(i => i.Account).Include(i => i.Account.Guardian)
            .Where(i => i.Period == period && !i.IsDeleted)
            .Select(i => i);

        public async Task BulkCompleteInvoiceRun(string period, Guid completedById)
        {
            var invoiceRunToComplete = _context.InvoiceRuns.SingleOrDefault(ir => ir.Period == period && !ir.Complete);
            if (invoiceRunToComplete != null)
            {
                invoiceRunToComplete.Complete = true;
                invoiceRunToComplete.CompletedById = completedById;
                invoiceRunToComplete.DateCompleted = DateTime.Now;

                _context.InvoiceRuns.Update(invoiceRunToComplete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public bool InvoiceRunComplete(string period) => _context.InvoiceRuns.SingleOrDefault(ir => ir.Period == period)?.Complete ?? false;

        public InvoiceRun GetInvoiceRunForCurrentPeriod(string period) =>
            _context.InvoiceRuns.SingleOrDefault(ir => ir.Period == period);

        public IEnumerable<InvoiceDisplayViewModel> GetAccountInvoices(Guid accountId) =>
            from invoiceRun in _context.InvoiceRuns
            join invoice in _context.Invoices on invoiceRun.Id equals invoice.InvoiceRunId
            join account in _context.Accounts on invoice.AccountId equals account.Id
            orderby invoiceRun.Period descending 
            select new InvoiceDisplayViewModel
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNo,
                AccountNumber = account.AccountNo,
                Period = invoiceRun.Period,
                TotalExcludingVat = invoice.TotalExcludingVat.ToString("C"),
                TotalVat = invoice.TotalVat.ToString("C"),
                TotalIncludingVat = invoice.TotalIncludingVat.ToString("C")
            };

        public InvoiceDetailViewModel GetInvoiceReport(Guid invoiceId)
        {
            var invoice = _context.Invoices
                .Include(i => i.Account)
                .Include(i => i.School)
                .Include(i => i.InvoiceItems)
                .Include(i => i.Account.Guardian)
                .FirstOrDefault(i => i.Id == invoiceId);
            if (invoice == null) return null;

            return new InvoiceDetailViewModel
            {
                Id = invoice.Id, 
                AccountDetails = new AccountDetailViewModel
                {
                    Id = invoice.Account.Id, 
                    AccountNumber = invoice.Account.AccountNo, 
                    AccountStatusId = invoice.Account.AccountStatusId, 
                    AccountTypeId = invoice.Account.AccountTypeId, 
                    Current = invoice.Account.Current, 
                    CurrentPeriod = invoice.Account.CurrentPeriod, 
                    GuardianId = invoice.Account.GuardianId, 
                    HundredTwentyDays = invoice.Account.HundredTwentyDays, 
                    Inactive = invoice.Account.IsDeleted, 
                    NinetyDays = invoice.Account.NinetyDays, 
                    SixtyDays = invoice.Account.SixtyDays, 
                    ThirtyDays = invoice.Account.ThirtyDays, 
                    Guardian = new GuardianDetailViewModel
                    {
                        Id = invoice.Account.Guardian.Id, 
                        Name = $@"{invoice.Account.Guardian.FirstName} {invoice.Account.Guardian.LastName}", 
                        Email = invoice.Account.Guardian.Email, 
                        HomePhoneNumber = invoice.Account.Guardian.HomePhoneNumber, 
                        MobileNumber = invoice.Account.Guardian.MobileNumber
                    }
                }, 
                InvoiceNumber = invoice.InvoiceNo, 
                InvoiceDate = invoice.CreatedDate,
                Period = invoice.Period, 
                SchoolDetails = new SchoolDetailViewModel
                {
                    Id = invoice.School.Id, 
                    Name = invoice.School.Name, 
                    Inactive = invoice.School.IsDeleted, 
                    ContactEmail = invoice.School.Email, 
                    ContactNumber = invoice.School.PhoneNumber, 
                    ContactPerson = invoice.School.ContactPerson
                },
                TotalExcludingVat = invoice.TotalExcludingVat, 
                TotalVat = invoice.TotalVat, 
                TotalIncludingVat = invoice.TotalIncludingVat, 
                AccountId = invoice.AccountId, 
                Inactive = invoice.IsDeleted, 
                DateSettled = invoice.DateSettled, 
                SchoolId = invoice.SchoolId, 
                Settled = invoice.Settled, 
                InvoiceItems = new List<InvoiceItemDetailViewModel>(invoice.InvoiceItems.Select(ii => new InvoiceItemDetailViewModel
                {
                    Id = ii.Id, 
                    Inactive = ii.IsDeleted, 
                    Description = ii.Description, 
                    InvoiceId = ii.InvoiceId, 
                    ItemCharge = ii.ItemCharge, 
                    ItemTotal = ii.ItemTotal, 
                    VatAmount = ii.VatAmount
                }))
            };
        }
    }
}
