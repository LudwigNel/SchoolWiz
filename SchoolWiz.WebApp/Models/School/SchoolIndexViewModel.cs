using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.School
{
    public class SchoolIndexViewModel : AuditModelBase
    {
        [Required, StringLength(1000), Display(Name = "School Name")]
        public string Name { get; set; }
        [Display(Name = "VAT Number")]
        public string VatNo { get; set; }
        [Display(Name = "Company Registration Number")]
        public string RegistrationNo { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Required, StringLength(15), Display(Name = "Contact Phone Number")]
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.EmailAddress), StringLength(5000)]
        public string Email { get; set; }
        [Display(Name = "School Logo")]
        public byte[] ImageUrl { get; set; }
        [Display(Name = "InActive")]
        public bool IsDeleted { get; set; }
    }
}
