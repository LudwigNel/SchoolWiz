using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class Invoice :EntityBase
    {
        [Required]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        [Required, MaxLength(9)]
        public string InvoiceNo { get; set; }
        [Required]
        public Guid SchoolId { get; set; }
        public virtual School School { get; set; }
        [Required, MaxLength(6)]
        public string Period { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalExcludingVat { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalVat { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalIncludingVat { get; set; }
        public bool Settled { get; set; }
        public DateTime? DateSettled { get; set; }
        public Guid? InvoiceRunId { get; set; }
        public  virtual InvoiceRun InvoiceRun { get; set; }

        public virtual ICollection<AccountRate> AccountRates { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
