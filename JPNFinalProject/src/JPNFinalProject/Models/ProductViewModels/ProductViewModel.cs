﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        //public CategoryViewModel Category { get; set; }
        public string Description { get; set; }
        public string ItemNumber { get; set; }
        public int Amount { get; set; }
        //public BusinessViewModel Business { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public int PointGain { get; set; }
        public string ImageSource { get; set; }

        public List<ProductViewModel> ProductList { get; set; } //Denne skal slettes, det er kun til testing

    }
}