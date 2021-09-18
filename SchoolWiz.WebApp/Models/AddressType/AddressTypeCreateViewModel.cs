using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.AddressType
{
    public class AddressTypeCreateViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
