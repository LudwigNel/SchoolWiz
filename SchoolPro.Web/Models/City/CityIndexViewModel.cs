using System;

namespace SchoolWiz.Web.Models.City
{
    public class CityIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public bool InActive { get; set; }
    }
}
