using Microsoft.AspNetCore.Authorization.Infrastructure;
using SchoolWiz.WebApp.Models.Base;

namespace SchoolWiz.WebApp.Models.Guardian
{
    public class GuardianIndexViewModel : BaseIndexViewModel
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public override string Name => GetFullName();
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        private string GetFullName()
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(MiddleName) &&
                !string.IsNullOrWhiteSpace(LastName))
                return $@"{FirstName.Trim()} {MiddleName.Trim()} {LastName.Trim()}";

            return $@"{FirstName?.Trim()} {LastName?.Trim()}".Trim();
        }
    }
}
