using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class Province : EntityBase
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
