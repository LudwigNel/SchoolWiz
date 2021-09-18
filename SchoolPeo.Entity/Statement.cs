using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class Statement : EntityBase
    {
        [Required]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        [Required, MaxLength(6)]
        public string Period { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalExcludingVat { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalVat { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal TotalIncludingVat { get; set; }
    }
}
