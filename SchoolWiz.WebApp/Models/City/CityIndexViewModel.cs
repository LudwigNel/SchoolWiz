using System;
using SchoolWiz.WebApp.Models.Base;

namespace SchoolWiz.WebApp.Models.City
{
    public class CityIndexViewModel : BaseIndexViewModel
    {
        public Guid ProvinceId { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}
