using System.ComponentModel.DataAnnotations;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.Grade
{
    public class GradeEditViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
