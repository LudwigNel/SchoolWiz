using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.AddressType
{
    public class AddressTypeCreateViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
