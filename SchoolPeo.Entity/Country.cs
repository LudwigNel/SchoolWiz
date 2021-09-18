using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class Country : EntityBase
    {
        [Required, MaxLength(255)]
        [Column(Order = 1)]
        public string Name { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
