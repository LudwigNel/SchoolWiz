using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.AddressType
{
    public class AddressTypeEditViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
