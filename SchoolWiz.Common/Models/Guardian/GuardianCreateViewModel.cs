using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models.Address;

namespace SchoolWiz.Common.Models.Guardian
{
    public class  GuardianCreateViewModel : AuditModelBase
    {
        [Required, StringLength(13), Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }

        [Required, StringLength(255), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(255), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required, StringLength(255), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Guardian Type")]
        public Guid GuardianTypeId { get; set; }

        public SelectList GuardianTypes { get; set; }

        [StringLength(15), Display(Name = "Home Phone Number")]
        public string HomePhoneNumber { get; set; }

        [StringLength(15), Display(Name = "Work Phone Number")]
        public string WorkPhoneNumber { get; set; }

        [Required, StringLength(15), Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        
        public Guid[] Students { get; set; }
        public SelectList StudentList { get; set; }

        [Display(Name = "Primary Contact")]
        public bool MainGuardian { get; set; }

        public AddressEntryViewModel AddressDetail { get; set; }
    }
}
