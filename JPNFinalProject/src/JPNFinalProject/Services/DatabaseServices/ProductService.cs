using JPNFinalProject.Data.DatabaseBrokers;
using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services.DatabaseServices
{
    public class ProductService
    {
        ProductBroker broker;
        public ProductService()
        {
            broker = new ProductBroker();
        }
        public List<ProductDTO> GetAllProducts()
        {
            using (var db = new JPNFinalProjectContext())
            {
                var temp = db.Product.ToList();
            }
            var tempList = broker.GetAllProducts();
            return null;
        }
    }
}
