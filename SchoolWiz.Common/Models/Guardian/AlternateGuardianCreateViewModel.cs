using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models.Address;

namespace SchoolWiz.Common.Models.Guardian
{
    public class AlternateGuardianCreateViewModel :AuditModelBase
    {
        [StringLength(13), Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }

        [StringLength(255), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(255), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [StringLength(255), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Guardian Type")]
        public Guid GuardianTypeId { get; set; }

        public SelectList GuardianTypes { get; set; }

        [StringLength(15), Display(Name = "Home Phone Number")]
        public string HomePhoneNumber { get; set; }

        [StringLength(15), Display(Name = "Work Phone Number")]
        public string WorkPhoneNumber { get; set; }

        [StringLength(15), Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public AddressEntryViewModel AddressDetail { get; set; }

        public bool HasValue()
        {
            return !string.IsNullOrEmpty(IdentityNumber) ||
                   !string.IsNullOrEmpty(FirstName) ||
                   !string.IsNullOrEmpty(MiddleName) ||
                   !string.IsNullOrEmpty(LastName) ||
                   GuardianTypeId != Guid.Empty ||
                   !string.IsNullOrEmpty(HomePhoneNumber) ||
                   !string.IsNullOrEmpty(WorkPhoneNumber) ||
                   !string.IsNullOrEmpty(MobileNumber) ||
                   !string.IsNullOrEmpty(Email);
        }
    }
}
