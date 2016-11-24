using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class Address
    {
        public Address()
        {
            Business = new HashSet<Business>();
            Person = new HashSet<Person>();
        }

        public int AddressId { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Business> Business { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
