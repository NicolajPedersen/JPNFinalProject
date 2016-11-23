using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public CategoryDTO Category { get; set; }
        public string Description { get; set; }
        public string ItemNumber { get; set; }
        public int Amount { get; set; }
        public int StockAmount { get; set; }
        public BusinessDTO Business { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public int PointGain { get; set; }
        public string ImageSource { get; set; }
        public bool OutOfStock { get; set; }
        public bool PutAside { get; set; }


        //Kun til testing
        public string mainCategory { get; set; }
        public string subCategory { get; set; }
        public string subsubCategory { get; set; }

    }
}
