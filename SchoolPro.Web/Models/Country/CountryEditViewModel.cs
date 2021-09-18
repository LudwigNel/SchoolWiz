using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.Country
{
    public class CountryEditViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
