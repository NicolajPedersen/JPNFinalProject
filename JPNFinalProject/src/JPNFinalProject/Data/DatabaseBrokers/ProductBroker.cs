using JPNFinalProject.Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DatabaseBrokers
{
    public class ProductBroker : JPNFinalProjectContext
    {
        public virtual List<Product> GetAllProducts()
        {
                return Product.ToList();
        }
    }
}
