using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class Business
    {
        public Business()
        {
            BusinessOrder = new HashSet<BusinessOrder>();
            BusinessProduct = new HashSet<BusinessProduct>();
        }

        public int BusinessId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OperationalHour { get; set; }

        public virtual ICollection<BusinessOrder> BusinessOrder { get; set; }
        public virtual ICollection<BusinessProduct> BusinessProduct { get; set; }
        public virtual Address Address { get; set; }
    }
}
