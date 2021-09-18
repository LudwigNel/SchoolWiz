using System;
using System.ComponentModel.DataAnnotations;
using SchoolWiz.WebApp.Models.Base;

namespace SchoolWiz.WebApp.Models.Vat
{
    public class VatIndexViewModel : BaseIndexViewModel
    {
        public decimal Value { get; set; }
        [Display(Name = "Valid From")]
        public string ValidFrom { get; set; }
        [Display(Name = "Valid To")]
        public string ValidTo { get; set; }
    }
}
