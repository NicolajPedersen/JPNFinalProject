using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("ParentProductCategory")]
        public int? Parent { get; set; }
        public string ProductText { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ProductCategory ParentProductCategory { get; set; }
        public virtual ICollection<ProductCategory> Children { get; set; }

    }
}
