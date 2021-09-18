using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.WebApp.Models.Student
{
    public class StudentIndexViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Name")] 
        public string Name => GetFullName();
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "InActive")]
        public bool InActive { get; set; }

        private string GetFullName()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(MiddleName) &&
                !string.IsNullOrEmpty(LastName))
                return $@"{FirstName.Trim()} {MiddleName.Trim()} {LastName.Trim()}";

            return $@"{FirstName?.Trim()} {LastName?.Trim()}".Trim();
        }
    }
}
