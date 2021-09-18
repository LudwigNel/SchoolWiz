using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models.Rate;

namespace SchoolWiz.Common.Models.AccountRate
{
    public class AccountEditAccountRateViewModel : AuditModelBase
    {
        public Guid AccountId { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name ="Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Discount %")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }

        [Required, Display(Name = "Rates")]
        public Guid[] RateIds { get; set; }
        public SelectList Rates { get; set; }
    }
}
