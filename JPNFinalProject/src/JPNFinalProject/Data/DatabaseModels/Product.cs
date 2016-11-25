using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class Product
    {
        public Product()
        {
            BusinessProduct = new HashSet<BusinessProduct>();
            OrderProduct = new HashSet<OrderProduct>();
            ProductCategory = new ProductCategory();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public string Description { get; set; }
        public string ItemNumber { get; set; }
        public int Amount { get; set; }
        public int Business { get; set; }
        public DateTimeOffset DeliveryTime { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<BusinessProduct> BusinessProduct { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
