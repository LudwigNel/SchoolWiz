using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Common.Models.Rate
{
    public class RateCreateViewModel : AuditModelBase
    {
        [Required, StringLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal Value { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }
}
