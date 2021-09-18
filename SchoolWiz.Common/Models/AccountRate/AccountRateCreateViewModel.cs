using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models.Rate;

namespace SchoolWiz.Common.Models.AccountRate
{
    public class AccountRateCreateViewModel : AuditModelBase
    {
        public Guid AccountId { get; set; }

        [Required, Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required, Display(Name = "Account")] 
        public string AccountName { get; set; }

        [Required, Display(Name = "Rates")] 
        public IEnumerable<RateCreateViewModel> Rates { get; set; }

        [Display(Name = "Discount %")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }
    }
}
