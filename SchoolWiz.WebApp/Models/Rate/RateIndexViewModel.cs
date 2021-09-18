using System;
using SchoolWiz.WebApp.Models.Base;

namespace SchoolWiz.WebApp.Models.Rate
{
    public class RateIndexViewModel : BaseIndexViewModel
    {
        public string Description { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Value { get; set; }
    }
}
