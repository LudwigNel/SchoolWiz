using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.Country
{
    public class CountryCreateViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
