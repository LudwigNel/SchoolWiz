using System;
using System.Collections.Generic;
using SchoolWiz.Common.Models.Account;
using SchoolWiz.Common.Models.School;

namespace SchoolWiz.Common.Models.Invoice
{
    public class InvoiceDetailViewModel : AuditModelBase
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public Guid SchoolId { get; set; }
        public SchoolDetailViewModel SchoolDetails { get; set; }
        
        public Guid AccountId { get; set; }
        public AccountDetailViewModel AccountDetails { get; set; }

        public string Period { get; set; }

        public decimal TotalExcludingVat { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalIncludingVat { get; set; }

        public bool Settled { get; set; }
        public DateTime? DateSettled { get; set; }

        public List<InvoiceItemDetailViewModel> InvoiceItems { get; set; }
    }
}
