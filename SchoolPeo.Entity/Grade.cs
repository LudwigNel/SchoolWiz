using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolWiz.Entity
{
    public class Grade : EntityBase
    {
        [Required]
        [Column(Order = 1)]
        public string Name { get; set; }
    }
}
