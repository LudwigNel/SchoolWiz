using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.WebApp.Models.Account
{
    public class AccountIndexViewmodel
    {
        public Guid Id { get; set; }

        [Display(Name = "Account Number")]
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
    }
}
