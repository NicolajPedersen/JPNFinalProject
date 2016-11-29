using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class DBProductMapper
    {
        public static ProductDTO DBProductToProductDTO(Product input, ProductCategory category)
        {
            ProductDTO dto = new ProductDTO();
            dto.Id = input.ProductId;
            dto.Name = input.Name;
            dto.Amount = input.Amount;
            dto.Price = Convert.ToDouble(input.Price);
            dto.Description = input.Description;
            dto.ImageSource = input.ImagePath;
            dto.Category = CategoryMapper.DBCategoryToCategoryDTO(category);

            dto.PointGain = Convert.ToInt32(dto.Price/10); //Dette skal udregnes fra pris. 1 point pr 10 kr.
            

            return dto;
        }

        public static List<ProductDTO> ProductsToListOfProductDTOs(List<Product> input) {
            List<ProductDTO> products = new List<ProductDTO>();

            foreach (var product in input) {
                ProductDTO dto = new ProductDTO();
                dto.Id = product.ProductId;
                dto.Name = product.Name;
                dto.Amount = product.Amount;
                dto.Price = Convert.ToDouble(product.Price);
                dto.Description = product.Description;
                dto.ImageSource = product.ImagePath;
                dto.PointGain = Convert.ToInt32(dto.Price / 10);

                products.Add(dto);
            }

            return products;
        }
    }
}
