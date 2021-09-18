using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.AccountType
{
    public class AccountTypeEditViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
