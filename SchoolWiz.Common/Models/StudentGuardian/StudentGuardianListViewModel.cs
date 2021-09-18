using System;

namespace SchoolWiz.Common.Models.StudentGuardian
{
    public class StudentGuardianListViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Name => GetFullName();

        public string GuardianType { get; set; }

        public bool MainContact { get; set; }

        public bool InActive { get; set; }

        private string GetFullName()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(MiddleName) && !string.IsNullOrEmpty(LastName))
                return $@"{FirstName.Trim()} {MiddleName.Trim()} {LastName.Trim()}";

            return $@"{FirstName?.Trim()} {LastName?.Trim()}".Trim();

        }
    }
}
