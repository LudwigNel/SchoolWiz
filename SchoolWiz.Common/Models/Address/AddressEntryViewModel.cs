namespace SchoolWiz.Common.Models.Address
{
    public class AddressEntryViewModel
    {
        public string ElementPrefix { get; set; }
        public AddressCreateViewModel PhysicalAddress { get; set; }
        public AddressCreateViewModel PostalAddress { get; set; }
    }
}
