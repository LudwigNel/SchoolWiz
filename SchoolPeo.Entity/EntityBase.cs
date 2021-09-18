using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        [Required]
        [Column(Order = 995)]
        public Guid CreatedById { get; set; }
        [Required]
        [Column(Order = 996)]
        public DateTime CreatedDate { get; set; }
        [Column(Order = 997)]
        public Guid? ModifiedById { get; set; }
        [Column(Order = 998)]
        public DateTime? ModifiedDate { get; set; }
        [Column(Order = 999)]
        public bool IsDeleted { get; set; }
    }
}
