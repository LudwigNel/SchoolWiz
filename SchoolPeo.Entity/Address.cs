using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class Address : EntityBase
    {
        [MaxLength(150)]
        [Column(Order = 1)]
        public string UnitNumber { get; set; }

        [MaxLength(255)]
        [Column(Order = 2)]
        public string ComplexName { get; set; }

        [Required, MaxLength(2000)]
        [Column(Order = 3)]
        public string StreetAddress { get; set; }

        [Required, MaxLength(2000)]
        [Column(Order = 4)]
        public string Suburb { get; set; }

        [Required]
        [Column(Order = 5)]
        public Guid CityId { get; set; }
        public virtual City City { get; set; }

        [Required, MaxLength(8)]
        [Column(Order = 6)]
        public string PostalCode { get; set; }

        [Required]
        [Column(Order = 7)]
        public Guid AddressTypeId { get; set; }
        public virtual AddressType AddressType { get; set; }

        public virtual ICollection<GuardianAddress> GuardianAddresses { get; set; }
        public virtual ICollection<SchoolAddress> SchoolAddresses { get; set; }
    }
}
