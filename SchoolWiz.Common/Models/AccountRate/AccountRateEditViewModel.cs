using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Common.Models.AccountRate
{
    public class AccountRateEditViewModel : AuditModelBase
    {
        public Guid AccountId { get; set; }

        [Required, Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required, Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Rate")]
        public string RateDescription { get; set; }

        [Display(Name = "Fee")]
        public string Fee { get; set; }

        [Display(Name = "Discount %")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }
    }
}
