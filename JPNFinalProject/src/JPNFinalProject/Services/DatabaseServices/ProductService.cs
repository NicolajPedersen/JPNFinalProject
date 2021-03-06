﻿using JPNFinalProject.Data.DatabaseBrokers;
using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services.DBModelsMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ProductDTO GetProductById(int productId)
        {
            var dbProduct = broker.GetProductById(productId);

            var dto = DBProductMapper.DBProductToProductDTO(dbProduct, dbProduct.ProductCategory);

            return dto;
            
        }

        public List<ProductDTO> GetAllProducts()
        {
            //var tempList = broker.GetAllProducts();
            List<ProductDTO> productList = new List<ProductDTO>();

            var catList = broker.GetAllCategoriesWithProducts();

            foreach (var category in catList)
            {
                foreach (var product in category.Product)
                {
                    productList.Add(DBProductMapper.DBProductToProductDTO(product, category));
                }
            }

            return productList;
        }

        public ProductDTO GetRandomProduct()
        {
            var dbProducts = broker.GetAllProducts();

            var dto = new ProductDTO();

            Random ran = new Random();

            var ranNum = ran.Next(dbProducts.Count - 1);

            dto = DBProductMapper.DBProductToProductDTO(dbProducts[ranNum], dbProducts[ranNum].ProductCategory);

            return dto;


        }

        public List<CategoryDTO> GetAllCategories()
        {
            var tempList = broker.GetAllCategories();

            List<CategoryDTO> list = new List<CategoryDTO>();

            foreach (var category in tempList)
            {
                list.Add(CategoryMapper.DBCategoryToCategoryDTO(category));
            }

            return null;
        }

        public void AddRandomProduct()
        {
            var product = new Product();

            var ran = new Random();

            var length = 10;

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            var categories = broker.GetAllCategories();

            var catId = categories[ran.Next(categories.Count)].ProductCategoryId;

            product.Name = res.ToString();
            product.Amount = ran.Next(5, 200);
            product.Description = res.ToString();
            product.ImagePath = "ProductList_200x150.png";
            product.ItemNumber = "23347733";
            product.Price = ran.Next(100, 400);
            product.DeliveryTime = DateTimeOffset.Now;

            broker.AddRandomProduct(product, catId);

        }

        public void AddRandomCategory()
        {
            var category = new ProductCategory();

            var length = 10;

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            category.Name = res.ToString();
            category.ProductText = res.ToString() + res.ToString() + res.ToString();

            broker.AddCategory(category);

        }
    }
}
