using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class InvoiceRun : EntityBase
    {
        [Required]
        public string Period { get; set; }

        public bool Complete { get; set; }

        public DateTime? DateCompleted { get; set; }

        public Guid? CompletedById { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
