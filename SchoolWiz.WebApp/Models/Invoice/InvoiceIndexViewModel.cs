using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.WebApp.Models.Invoice
{
    public class InvoiceIndexViewModel
    {
        [Display(Name = "Invoice Period")]
        public string Period { get; set; }

        public bool Complete { get; set; }

        [Display(Name = "Completed By")]
        public string CompletedBy { get; set; }

        [Display(Name = "Date Completed")]
        public string CompletedDate { get; set; }

        public Guid CreatedById { get; set; }

        public IEnumerable<InvoiceIndexDetailViewModel> Invoices { get; set; }
    }
}
