using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class Account :EntityBase
    {
        [Required, MaxLength(6)]
        [Column(Order = 1)]
        public string AccountNo { get; set; }
        [Required]
        [Column(Order = 2)]
        public Guid GuardianId { get; set; }
        public virtual Guardian Guardian { get; set; }
        [Required]
        [Column(Order = 3)]
        public string CurrentPeriod { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)", Order = 4)]
        public decimal Current { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)", Order = 5)]
        public decimal ThirtyDays { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)", Order = 6)]
        public decimal SixtyDays { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)", Order = 7)]
        public decimal NinetyDays { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)", Order = 8)]
        public decimal HundredTwentyDays { get; set; }
        [Required]
        [Column(Order = 9)]
        public Guid AccountStatusId { get; set; }
        public virtual AccountStatus AccountStatus { get; set; }
        [Required]
        [Column(Order = 10)]
        public Guid AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
        
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
        public virtual ICollection<AccountRate> AccountRates { get; set; }
    }
}
