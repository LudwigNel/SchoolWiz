using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolWiz.Common.Models.School
{
    public class SchoolDetailViewModel : AuditModelBase
    {
        [Required]
        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }
    }
}
