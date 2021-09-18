using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SchoolWiz.Entity
{
    public class Guardian : EntityBase
    {
        [Required, MaxLength(13), 
         RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579] [26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))")]
        [Column(Order = 1)]
        public string IdentityNumber { get; set; }
        [Required, MaxLength(255)]
        [Column(Order = 2)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        [Column(Order = 3)]
        public string MiddleName { get; set; }
        [Required, MaxLength(255)]
        [Column(Order = 4)]
        public string LastName { get; set; }
        [Required]
        [Column(Order = 5)]
        public Guid GuardianTypeId { get; set; }
        public virtual GuardianType GuardianType { get; set; }
        [MaxLength(15)]
        [Column(Order = 6)]
        public string HomePhoneNumber { get; set; }
        [MaxLength(15)]
        [Column(Order = 7)]
        public string WorkPhoneNumber { get; set; }
        [Required, MaxLength(15)]
        [Column(Order = 8)]
        public string MobileNumber { get; set; }
        [MaxLength(5000)]
        [Column(Order = 9)]
        public string Email { get; set; }

        public virtual ICollection<GuardianAddress> GuardianAddresses { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<StudentGuardian> StudentGuardians { get; set; }
    }
}
