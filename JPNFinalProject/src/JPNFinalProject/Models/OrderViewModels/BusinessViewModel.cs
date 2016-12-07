using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPNFinalProject.Data.DTO;

namespace JPNFinalProject.Models.OrderViewModels
{
    public class BusinessViewModel
    {
        public BusinessViewModel()
        {
            Businesses = new List<BusinessDTO>();
        }
        public List<BusinessDTO> Businesses { get; set; }
    }
}
