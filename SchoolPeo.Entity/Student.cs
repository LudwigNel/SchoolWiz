using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class Student : EntityBase
    {
        [Required, MaxLength(25)]
        public string StudentNumber { get; set; }
        [Required, MaxLength(13),
         RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579] [26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))")]
        public string IdentityNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required, MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string MiddleName { get; set; }
        [Required, MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(15)] 
        public string MobileNumber { get; set; }
        [MaxLength(5000)]
        public string Email { get; set; }
        [Required] 
        public int Age { get; set; }
        [MaxLength(5000)]
        public string MedicalConditions { get; set; }
        [MaxLength(255)]
        public string Doctor { get; set; }
        [MaxLength(15)]
        public string DoctorPhoneNumber { get; set; }
        public bool UnderSupervisedMedication { get; set; }
        public bool MedicationCausesDrowsiness { get; set; }

        public virtual ICollection<StudentGuardian> StudentGuardians { get; set; }
    }
}
