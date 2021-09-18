using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class StudentGuardian : EntityBase
    {
        [Required]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        public Guid GuardianId { get; set; }
        public virtual Guardian Guardian { get; set; }

        public bool PrimaryContact { get; set; }
    }
}
