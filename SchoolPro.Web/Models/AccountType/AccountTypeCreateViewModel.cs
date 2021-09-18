using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.AccountType
{
    public class AccountTypeCreateViewModel :AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
