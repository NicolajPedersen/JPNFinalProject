using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.OrderViewModels
{
    public class PaymentDoneViewModel
    {
        public OrderDTO Order { get; set; }
        public double Subtotal { get; set; }
        public double VATFromPrice { get; set; }
        public double PriceWithVAT { get; set; }
        public bool IAcceptTheConditions { get; set; }
    }
}
