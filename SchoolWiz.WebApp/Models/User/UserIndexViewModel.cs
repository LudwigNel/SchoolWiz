using System;

namespace SchoolWiz.WebApp.Models.User
{
    public class UserIndexViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityNumber { get; set; }
        public bool InActive { get; set; }
    }
}
