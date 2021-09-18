using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWiz.Common.Models;

namespace SchoolWiz.WebApp.Models.Province
{
    public class ProvinceCreateViewModel : AuditModelBase
    {
        [Required]
        public string Name { get; set; }
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }
        public SelectList Countries { get; set; }
    }
}
