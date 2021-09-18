using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Common.Models.Student
{
    public class StudentCreateViewModel : AuditModelBase
    {
        [Required, StringLength(25), Display(Name = "Student Number")]
        public string StudentNumber { get; set; }

        [Required, StringLength(13), Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }

        [Required, Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(255), Display(Name = "First Name")] 
        public string FirstName { get; set; }

        [StringLength(255), Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required, StringLength(255), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(15), Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required, Display(Name = "Age"), Range(6, 21, ErrorMessage = "A student can only have an age between 6 - 21.")]
        public int Age { get; set; }

        [StringLength(5000), Display(Name = "Medical Conditions")]
        public string MedicalConditions { get; set; }

        [StringLength(255), Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [StringLength(15), Display(Name = "Doctor Contact Number")]
        public string DoctorContactNumber { get; set; }

        [Display(Name = "Under Supervised Medication")]
        public bool UnderSupervisedMedication { get; set; }

        [Display(Name = "Medication Causes Drowsiness")]
        public bool MedicationCausesDrowsiness { get; set; }
    }
}
