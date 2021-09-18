using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class AddressType : EntityBase
    {
        [Required, MaxLength(150)]
        [Column(Order = 1)]
        public string Name { get; set; }

        public virtual ICollection<AddressType> AddressTypes { get; set; }
    }
}
