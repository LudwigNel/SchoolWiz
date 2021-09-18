using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.GuardianType
{
    public class GuardianTypeCreateViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
