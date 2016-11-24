using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class Person
    {
        public Person()
        {
            Order = new HashSet<Order>();
        }

        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public int AddressId { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual Address Address { get; set; }
    }
}
