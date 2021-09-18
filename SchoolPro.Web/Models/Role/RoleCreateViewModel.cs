using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.Role
{
    public class RoleCreateViewModel :AuditModelBase
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public bool InActive { get; set; }
    }
}
