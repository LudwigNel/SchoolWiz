using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SchoolWiz.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
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

        [Required, MaxLength(15)]
        [Column(Order = 5)]
        public string MobileNumber { get; set; }

        [Required]
        [Column(Order = 6)]
        public Guid CreatedById { get; set; }

        [Required]
        [Column(Order = 7)]
        public DateTime DateCreated { get; set; }

        [Column(Order = 8)]
        public Guid? ModifiedById { get; set; }

        [Column(Order = 9)]
        public DateTime? ModifiedDate { get; set; }

        [Column(Order = 10)]
        public bool IsDeleted { get; set; }
    }
}
