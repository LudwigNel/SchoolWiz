using System;

namespace SchoolWiz.WebApp.Models.Base
{
    public class BaseIndexViewModel
    {
        public Guid Id { get; set; }
        public virtual string Name { get; set; }
        public bool InActive { get; set; }
    }
}
