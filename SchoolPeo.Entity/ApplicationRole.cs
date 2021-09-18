using System;
using Microsoft.AspNetCore.Identity;

namespace SchoolWiz.Entity
{
    public  class ApplicationRole: IdentityRole<Guid>
    {
        public bool IsDeleted { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedByDate { get; set; }
    }
}
