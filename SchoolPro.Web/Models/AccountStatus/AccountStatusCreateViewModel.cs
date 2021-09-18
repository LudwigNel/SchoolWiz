using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.AccountStatus
{
    public class AccountStatusCreateViewModel: AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
