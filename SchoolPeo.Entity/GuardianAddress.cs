using System;

namespace SchoolWiz.Entity
{
    public class GuardianAddress : EntityBase
    {
        public Guid GuardianId { get; set; }
        public Guardian Guardian { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}
