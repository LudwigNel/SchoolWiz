using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;
using SchoolWiz.WebApp.Models.Base;

namespace SchoolWiz.WebApp.Models.AccountType
{
    public class AccountTypeEditViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
