using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class AccountRate : EntityBase
    {
        [Required]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        [Required] 
        public Guid RateId { get; set; }
        public virtual Rate Rate { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal DiscountAmount { get; set; }
    }
}
