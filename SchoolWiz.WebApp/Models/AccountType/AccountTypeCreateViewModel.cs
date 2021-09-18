using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.AccountType
{
    public class AccountTypeCreateViewModel :AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
