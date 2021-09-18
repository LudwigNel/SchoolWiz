using System;

namespace SchoolWiz.Entity
{
    public class SchoolAddress : EntityBase
    {
        public Guid SchoolId { get; set; }
        public School School { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
