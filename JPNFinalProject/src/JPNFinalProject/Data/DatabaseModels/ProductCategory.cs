using System;
using System.Collections.Generic;

namespace JPNFinalProject.Data.DatabaseModels
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Product = new HashSet<Product>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        //public int Parent { get; set; }
        public string ProductText { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ProductCategory Parent { get; set; }
    }
}
