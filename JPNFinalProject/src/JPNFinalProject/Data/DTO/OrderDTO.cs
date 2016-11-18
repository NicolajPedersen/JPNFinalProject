using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public PersonDTO Person { get; set; }
        public string CustomerMail { get; set; }
        public BusinessDTO Business { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
