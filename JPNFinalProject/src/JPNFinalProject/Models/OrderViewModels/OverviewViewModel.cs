using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.OrderViewModels
{
    public class OverviewViewModel
    {
        public OrderDTO Order { get; set; }
        public double Subtotal { get; set; }
        public double VATFromPrice { get; set; }
        public bool IAcceptTheConditions { get; set; }
    }
}
