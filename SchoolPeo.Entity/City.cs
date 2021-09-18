using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class City : EntityBase
    {
        [Required, MaxLength(255)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [Required]
        [Column(Order = 2)]
        public Guid ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
