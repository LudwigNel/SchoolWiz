using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.School
{
    public class SchoolIndexViewModel
    {
        public Guid Id { get; set; }
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
        [Display(Name = "Company Logo")]
        public byte[] ImageUrl { get; set; }
        [Display(Name = "Inactive")]
        public bool IsDeleted { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
