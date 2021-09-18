using System;
using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.Vat
{
    public class VatCreateViewModel : AuditModelBase
    {
        [Required]
        public decimal Value { get; set; }
        [Display(Name = "Valid From")]
        public DateTime? ValidFrom { get; set; }
        [Display(Name = "Valid To")]
        public DateTime? ValidTo { get; set; }
    }
}
