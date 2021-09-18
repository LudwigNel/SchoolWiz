using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.WebApp.Models.Invoice
{
    public class InvoiceIndexDetailViewModel 
    {
        public Guid Id { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Amount Excl. VAT")]
        public string ExcludingVatTotal { get; set; }

        [Display(Name = "VAT")]
        public string VatAmount { get; set; }

        [Display(Name = "Amount Incl VAT")]
        public string IncludingVatTotal { get; set; }
    }
}
