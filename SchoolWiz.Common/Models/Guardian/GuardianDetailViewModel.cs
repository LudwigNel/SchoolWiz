using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Common.Models.Guardian
{
    public class GuardianDetailViewModel : AuditModelBase
    {
        public string IdentityNumber { get; set; }

        public string Name { get; set; }

        public string HomePhoneNumber { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Guid GuardianTypeId { get; set; }
        public string Guardiantype { get; set; }
    }
}
