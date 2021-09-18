using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class AccountStatus : EntityBase
    {
        [Required]
        [Column(Order = 1)]
        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
