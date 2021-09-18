using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models.AccountRate;
using SchoolWiz.Common.Models.Invoice;

namespace SchoolWiz.Common.Models.Account
{
    public class AccountEditViewModel : AuditModelBase
    {
        [Required, StringLength(6), Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Account Name")] 
        public string AccountName { get; set; }

        [Display(Name = "Current Period")]
        public string CurrentPeriod { get; set; }

        [Display(Name = "Current")]
        public decimal Current { get; set; }

        [Display(Name = "30 Days")]
        public decimal ThirtyDays { get; set; }

        [Display(Name = "60 Days")] 
        public decimal SixtyDays { get; set; }

        [Display(Name = "90 Days")] 
        public decimal NinetyDays { get; set; }

        [Display(Name = "120 Days")] 
        public decimal HundredTwentyDays { get; set; }

        [Display(Name = "Total Amount")]
        public decimal Total { get; set; }

        [Required, Display(Name = "Account Status")]
        public Guid AccountStatusId { get; set; }
        public SelectList AccountStatusList { get; set; }

        [Required, Display(Name = "Account Type")]
        public Guid AccountTypeId { get; set; }
        public SelectList AccountTypeList { get; set; }

        public IEnumerable<AccountRateDisplayViewmodel> AccountRates { get; set; }
        public IEnumerable<InvoiceDisplayViewModel> Invoices { get; set; }
    }
}
