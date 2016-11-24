using JPNFinalProject.Data.DatabaseBrokers;
using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services.DBModelsMappers;
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
            var tempList = broker.GetAllProducts();
            List<ProductDTO> productList = new List<ProductDTO>();

            foreach (var product in tempList)
            {
                productList.Add(DBProductMapper.DBProductToProductDTO(product));
            }
            return productList;
        }
    }
}
