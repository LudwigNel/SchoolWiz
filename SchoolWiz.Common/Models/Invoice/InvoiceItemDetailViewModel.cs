using System;

namespace SchoolWiz.Common.Models.Invoice
{
    public class InvoiceItemDetailViewModel : AuditModelBase
    {
        public Guid InvoiceId { get; set; }

        public string Description { get; set; }

        public decimal ItemCharge { get; set; }

        public decimal VatAmount { get; set; }

        public decimal ItemTotal { get; set; }
    }
}
