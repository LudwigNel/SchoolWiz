using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.User
{
    public class UserEditViewModel  :AuditModelBase
    {

        [Required, StringLength(256)] 
        public string UserName { get; set; }

        [Required, StringLength(13),
         RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579] [26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))")]
        [Column(Order = 1)]
        public string IdentityNumber { get; set; }

        [Required, StringLength(255)]
        [Column(Order = 2)]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Column(Order = 3)]
        public string MiddleName { get; set; }

        [Required, StringLength(255)]
        [Column(Order = 4)]
        public string LastName { get; set; }

        [Required, StringLength(15)]
        [Column(Order = 5)]
        public string MobileNumber { get; set; }

        [Required, StringLength(256)] 
        public string Email { get; set; }

        public bool InActive { get; set; }

        public string Roles { get; set; }

        [Column(Order = 10)]
        public bool IsDeleted { get; set; }
    }
}
