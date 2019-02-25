namespace CustomerAddressManager.Dal.Models
{
    public class Address
    {
        public string CustomerId { get; set; }
        public string AddressType { get; set; } //Could be defined in DB as dictionary table
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
