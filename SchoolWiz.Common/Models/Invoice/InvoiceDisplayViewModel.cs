using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Common.Models.Invoice
{
    public class InvoiceDisplayViewModel
    {
        public Guid Id { get; set; }

        public string SchoolName { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        public string Period { get; set; }

        public string TotalExcludingVat { get; set; }

        public string TotalVat { get; set; }

        public string TotalIncludingVat { get; set; }
    }
}
