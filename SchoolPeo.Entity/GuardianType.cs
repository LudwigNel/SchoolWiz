using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class GuardianType :EntityBase
    {
        [Required, MaxLength(150)]
        public string Name { get; set; }

        public virtual ICollection<Guardian> Guardians { get; set; }
    }
}
