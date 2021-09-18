using System;
using SchoolWiz.Common.Models.Guardian;

namespace SchoolWiz.Common.Models.Account
{
    public class AccountDetailViewModel : AuditModelBase
    {
        public string AccountNumber { get; set; }

        public Guid GuardianId { get; set; }
        public GuardianDetailViewModel Guardian { get; set; }

        public string CurrentPeriod { get; set; }

        public decimal Current { get; set; }

        public decimal ThirtyDays { get; set; }

        public decimal SixtyDays { get; set; }

        public decimal NinetyDays { get; set; }

        public decimal HundredTwentyDays { get; set; }

        public Guid AccountStatusId { get; set; }
        public string AccountStatus { get; set; }

        public Guid AccountTypeId { get; set; }
        public string Accounttype { get; set; }
    }
}
