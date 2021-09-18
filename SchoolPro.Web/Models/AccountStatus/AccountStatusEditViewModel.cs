using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.AccountStatus
{
    public class AccountStatusEditViewModel : AuditModelBase
    {
        [Required] public string Name { get; set; }
    }
}
