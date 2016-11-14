using JPNFinalProject.Data.DTO;
using JPNFinalProject.Models.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        public List<OrderDTO> OrderList { get; set; }
    }
}
