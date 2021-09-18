using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class InvoiceItem : EntityBase
    {
        [Required]
        public Guid InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal ItemCharge { get; set; }

        [Required]
        public Guid VatId { get; set; }
        public virtual Vat Vat { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal VatAmount { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal ItemTotal { get; set; }
    }
}
