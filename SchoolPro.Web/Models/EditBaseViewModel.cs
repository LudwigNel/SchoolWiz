using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Web.Models
{
    public class EditBaseViewModel
    {
        public Guid Id { get; set; }
        public bool Inactive { get; set; }
        public Guid CreatedById { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedById { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
