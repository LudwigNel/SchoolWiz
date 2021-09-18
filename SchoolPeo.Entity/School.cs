using System.ComponentModel.DataAnnotations;

namespace SchoolWiz.Entity
{
    public class School : EntityBase
    {
        [Required, MaxLength(1000)] 
        public string Name { get; set; }
        public string VatNo { get; set; }
        public string RegistrationNo { get; set; }
        public string ContactPerson { get; set; }
        [Required, MaxLength(15)] 
        public string PhoneNumber { get; set; }
        [Required, MaxLength(5000)] 
        public string Email { get; set; }
        public byte[] ImageUrl { get; set; }
    }
}
