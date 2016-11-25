using JPNFinalProject.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Data.DatabaseBrokers
{
    public class ProductBroker
    {
        public virtual List<Product> GetAllProducts()
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.Product.ToList();
            }

        }
        public virtual ProductCategory GetCategoryById(int id)
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.ProductCategory.Where(x => x.ProductCategoryId == id).Include(x => x.ParentProductCategory).Single();
            }

        }
        public virtual List<ProductCategory> GetAllCategories()
        {
            using (var context = new JPNFinalProjectContext())
            {
                return context.ProductCategory
                    .Include(x => x.Product)
                    .Include(x => x.ParentProductCategory)
                    .ToList();
            }

        }
    }
}
