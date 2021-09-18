using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolWiz.Common.Models.Invoice;
using SchoolWiz.Entity;

namespace SchoolWiz.Services
{
    public interface IInvoiceService
    {
        Task GenerateInvoices(Guid createdById);
        Invoice GetById(Guid id);
        IEnumerable<Invoice> GetInvoicesForCurrentPeriod(string period);
        Task BulkCompleteInvoiceRun(string period, Guid completedById);
        bool InvoiceRunComplete(string period);
        InvoiceRun GetInvoiceRunForCurrentPeriod(string period);
        IEnumerable<InvoiceDisplayViewModel> GetAccountInvoices(Guid accountId);
        InvoiceDetailViewModel GetInvoiceReport(Guid invoiceId);
    }
}
