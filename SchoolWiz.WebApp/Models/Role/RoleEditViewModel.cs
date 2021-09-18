using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.Role
{
    public class RoleEditViewModel  :AuditModelBase
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public bool InActive { get; set; }
    }
}
