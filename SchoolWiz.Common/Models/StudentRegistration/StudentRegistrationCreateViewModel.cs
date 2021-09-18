using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Enums;
using SchoolWiz.Common.Models.Guardian;
using SchoolWiz.Common.Models.Student;

namespace SchoolWiz.Common.Models.StudentRegistration
{
    public class StudentRegistrationCreateViewModel : AuditModelBase
    {
        public StudentCreateViewModel Student { get; set; }

        public GuardianCreateViewModel MainGuardian { get; set; }
        public AlternateGuardianCreateViewModel AlternateGuardian { get; set; }

        [Required, Display(Name = "Grade")] 
        public Guid GradeId { get; set; }

        public SelectList Grades { get; set; }

        [Required, Display(Name = "School Year")]
        public int SchoolYear { get; set; }

        [Required, Display(Name = "Billing Cycle")]
        public Guid AccountTypeId { get; set; }
        public SelectList AccountTypes { get; set; }

        public bool DisplayToastMessage { get; set; }
        public ToastType ToastType { get; set; }
        public string ToastMessage { get; set; }
        public string ToastTitle { get; set; }

        public StudentRegistrationCreateViewModel()
        {
            Student = new StudentCreateViewModel();
            MainGuardian = new GuardianCreateViewModel();
            AlternateGuardian = new AlternateGuardianCreateViewModel();
        }
    }
}
