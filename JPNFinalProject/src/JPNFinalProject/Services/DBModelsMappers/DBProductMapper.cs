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
        public static ProductDTO DBProductToProductDTO(Product input)
        {
            ProductDTO dto = new ProductDTO();
            dto.Id = input.ProductId;
            dto.Name = input.Name;
            dto.Price = Convert.ToDouble(input.Price);
            dto.PointGain = 25; //Dette skal fås fra db
            dto.Description = input.Description;
            dto.ImageSource = "ProductList_200x150.png";
            //dto.Category = input.ProductCategory;

            dto.Category = CategoryMapper.DBCategoryToCategoryDTO(input.ProductCategory);

            return dto;
        }
    }
}
