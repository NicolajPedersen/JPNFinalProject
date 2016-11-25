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
                var category = broker.GetCategoryById(product.ProductCategoryId);
                productList.Add(DBProductMapper.DBProductToProductDTO(product, category));
            }
            return productList;
        }

        public List<CategoryDTO> GetAllCategories()
        {
            var tempList = broker.GetAllCategories();



            return null;
        }
    }
}
