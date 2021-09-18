using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWiz.Web.Models.City
{
    public class CityEditViewModel : AuditModelBase
    {
        public string Name { get; set; }
        public bool InActive { get; set; }
        [Display(Name = "Province")]
        public Guid ProvinceId { get; set; }
        public SelectList Countries { get; set; }
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
    }
}
