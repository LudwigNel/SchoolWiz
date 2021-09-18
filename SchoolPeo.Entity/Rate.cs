using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class Rate : EntityBase
    {
        [Required, MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal Value { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public virtual ICollection<AccountRate> AccountRates { get; set; }
    }
}
