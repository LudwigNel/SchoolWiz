using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class StudentRegistration : EntityBase
    {
        [Required]
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        private ICollection<StudentGuardian> StudentGuardians { get; set; }

        [Required]
        public Guid GradeId { get; set; }
        public virtual Grade Grade { get; set; }

        [Required]
        public int SchoolYear { get; set; }
    }
}
