using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.OrderViewModels
{
    public class BasketViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public int SelectedDeliveryType { get; set; }
    }
}
