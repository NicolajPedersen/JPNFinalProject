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
            dto.Price = Convert.ToDouble(input.Price);
            dto.PointGain = 25; //Dette skal udregnes fra pris. 1 point pr 10 kr.
            dto.Description = input.Description;
            dto.ImageSource = input.ImagePath;
            //dto.Category = input.ProductCategory;

            dto.Category = CategoryMapper.DBCategoryToCategoryDTO(category);

            return dto;
        }
    }
}
