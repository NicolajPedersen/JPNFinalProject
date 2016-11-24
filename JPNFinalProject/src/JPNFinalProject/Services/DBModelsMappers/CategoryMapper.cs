using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPNFinalProject.Data.DatabaseModels;
using JPNFinalProject.Data.DTO;

namespace JPNFinalProject.Services.DBModelsMappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO DBCategoryToCategoryDTO(ProductCategory input)
        {
            CategoryDTO dto = new CategoryDTO();

            dto.Id = input.ProductCategoryId;
            dto.Name = input.Name;
            dto.ProductText = input.ProductText;
            //dto.ParentCategory = input.Parent;

            return dto;
        }
    }
}
