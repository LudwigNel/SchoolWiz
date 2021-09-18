using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models.Country
{
    public class CountryCreateViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
