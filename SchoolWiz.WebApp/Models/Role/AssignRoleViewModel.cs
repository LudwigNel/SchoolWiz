using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.Role
{
    public class AssignRoleViewModel : AuditModelBase
    {
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public Guid[] SelectedRoles { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public SelectList RoleList { get; set; }
    }
}
