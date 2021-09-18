using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWiz.Common.Models.Address
{
    public class AddressCreateViewModel : AuditModelBase, IEquatable<AddressCreateViewModel>
    {
        [Display(Name = "Unit Number"), StringLength(150)]
        public string UnitNumber { get; set; }

        [Display(Name = "Complex"), StringLength(255)] 
        public string ComplexName { get; set; }

        [Required]
        [Display(Name = "Street Address"), StringLength(255)]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public Guid CityId { get; set; }
        public SelectList Cities { get; set; }

        [Display(Name = "Address Type")] 
        public Guid AddressTypeId { get; set; }
        public SelectList AddressTypes { get; set; }

        [Required]
        [Display(Name = "Suburb"), StringLength(255)]
        public string Suburb { get; set; }

        [Required]
        [Display(Name = "Province"), StringLength(255)] 
        public string Province { get; set; }

        [Required]
        [Display(Name = "Country"), StringLength(255)]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Postal Code"), StringLength(8)]
        public string PostalCode { get; set; } = "0";

        public bool HasValue()
        {
            return !string.IsNullOrEmpty(UnitNumber) ||
                   !string.IsNullOrEmpty(ComplexName) ||
                   !string.IsNullOrEmpty(StreetAddress) ||
                   !string.IsNullOrEmpty(Suburb) ||
                   CityId != Guid.Empty ||
                   !string.IsNullOrEmpty(PostalCode);

        }

        public bool Equals(AddressCreateViewModel other)
        {
            if (other == null)
                return false;

            return
                (ReferenceEquals(UnitNumber, other.UnitNumber) || UnitNumber != null && UnitNumber.Equals(other.UnitNumber)) &&
                (ReferenceEquals(ComplexName, other.ComplexName) || ComplexName != null && ComplexName.Equals(other.ComplexName)) &&
                (ReferenceEquals(StreetAddress, other.StreetAddress) || StreetAddress != null && StreetAddress.Equals(other.StreetAddress)) &&
                (ReferenceEquals(Suburb, other.Suburb) || Suburb != null && Suburb.Equals(other.Suburb)) &&
                CityId.Equals(other.CityId) &&
                (ReferenceEquals(PostalCode, other.PostalCode) || PostalCode != null && PostalCode.Equals(other.PostalCode)) && 
                AddressTypeId.Equals(other.AddressTypeId);
        }

        public bool EqualsWithoutType(AddressCreateViewModel other)
        {
            if (other == null)
                return false;

            return
                (ReferenceEquals(UnitNumber, other.UnitNumber) || UnitNumber != null && UnitNumber.Equals(other.UnitNumber)) &&
                (ReferenceEquals(ComplexName, other.ComplexName) || ComplexName != null && ComplexName.Equals(other.ComplexName)) &&
                (ReferenceEquals(StreetAddress, other.StreetAddress) || StreetAddress != null && StreetAddress.Equals(other.StreetAddress)) &&
                (ReferenceEquals(Suburb, other.Suburb) || Suburb != null && Suburb.Equals(other.Suburb)) &&
                CityId.Equals(other.CityId) &&
                (ReferenceEquals(PostalCode, other.PostalCode) || PostalCode != null && PostalCode.Equals(other.PostalCode));
        }
    }
}
