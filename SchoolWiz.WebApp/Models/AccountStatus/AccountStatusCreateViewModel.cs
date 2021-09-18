using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.AccountStatus
{
    public class AccountStatusCreateViewModel: AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
