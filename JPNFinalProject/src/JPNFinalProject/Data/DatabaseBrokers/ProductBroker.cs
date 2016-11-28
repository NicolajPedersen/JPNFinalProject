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

        public virtual void AddRandomProduct(Product input, int CategoryID)
        {
            using (var context = new JPNFinalProjectContext())
            {
                var cat = context.ProductCategory.FirstOrDefault(x => x.ProductCategoryId == CategoryID);
                input.ProductCategory = cat;
                context.Product.Add(input);
                context.SaveChanges();
            }
        }

        public virtual void AddCategory(ProductCategory category)
        {
            using (var context = new JPNFinalProjectContext())
            {              
                context.Add(category);
                context.SaveChanges();
            }
        }
    }
}
