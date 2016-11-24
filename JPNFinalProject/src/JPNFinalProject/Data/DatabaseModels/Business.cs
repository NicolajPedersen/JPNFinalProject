using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class Business
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OperationalHour { get; set; }

        public virtual Address Address { get; set; }
    }
}
