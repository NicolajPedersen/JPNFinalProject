﻿using JPNFinalProject.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductList = new List<ProductDTO>();
            CategoryList = new List<CategoryDTO>();
            MainCategoryList = new List<CategoryDTO>();
            SubCategoryList = new List<CategoryDTO>();
        }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public double Price { get; set; }
        //public CategoryDTO Category { get; set; }
        //public string Description { get; set; }
        //public string ItemNumber { get; set; }
        //public int Amount { get; set; }
        ////public BusinessViewModel Business { get; set; }
        //public DateTimeOffset DeliveryDate { get; set; }
        //public int PointGain { get; set; }
        //public string ImageSource { get; set; }
        //public bool OutOfStock { get; set; }

        public string ProductText { get; set; }
        public string ActiveCategory { get; set; }
        public List<ProductDTO> ProductList { get; set; }
        public List<CategoryDTO> CategoryList { get; set; }
        public List<CategoryDTO> MainCategoryList { get; set; }
        public List<CategoryDTO> SubCategoryList { get; set; }

    }
}
