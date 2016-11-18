using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DTO
{
    public class BusinessDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OperationalHour { get; set; }
    }
}
