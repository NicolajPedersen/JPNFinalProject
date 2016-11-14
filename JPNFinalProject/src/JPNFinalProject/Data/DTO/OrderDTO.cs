using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DTO
{
    public class OrderDTO
    {
        public int OrderNumber { get; set; }
        public string CustomerMail { get; set; }
        public List<ProductDTO> ProductList { get; set; }
    }
}
