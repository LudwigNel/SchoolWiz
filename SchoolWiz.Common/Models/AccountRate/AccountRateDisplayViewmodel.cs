using System;

namespace SchoolWiz.Common.Models.AccountRate
{
    public class AccountRateDisplayViewmodel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Fee { get; set; }

        public string DiscountPercentage { get; set; }

        public string DiscountAmount { get; set; }

        public bool InActive { get; set; }
    }
}
