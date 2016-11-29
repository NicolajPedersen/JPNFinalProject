using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.ProductViewModels;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using JPNFinalProject.Services.DatabaseServices;

namespace JPNFinalProject.Controllers
{
    public class ProductController : Controller
    {
        public static List<ProductDTO> productList;
        private SessionContainer _sessionContainer;
        private ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
            productList = new List<ProductDTO>()
            {
               new ProductDTO()
               {
                   Id = 1, Name = "Remington HC5600 E51 Pro Power Hair Clipper", Price = 300.00, PointGain = 30, Description = "Remington HC5600 E51 Pro Power Hair Clipper", ImageSource = "ProductList_200x150.png", Category = new CategoryDTO() { Id = 2, Name = "Haarklippere", ParentCategory = new CategoryDTO() { Id = 0, Name = "Barbering" } }
               },
               new ProductDTO()
               {
                   Id = 2, Name = "Remington Pro Power Hårklipper HC5200", Price = 249.00, PointGain = 25, Amount = 0, Description = "Remington Pro Power Hårklipper HC5200", ImageSource = "ProductList_200x150 (1).png", Category = new CategoryDTO() { Id = 2, Name = "Haarklippere", ParentCategory = new CategoryDTO() { Id = 0, Name = "Barbering" } }
               },
               new ProductDTO()
               {
                   Id = 3, Name = "Remington Apprentice Hårklipper", Price = 199.00, PointGain = 20, Description = "Remington Apprentice Hårklipper", ImageSource = "ProductList_200x150 (2).png", Category = new CategoryDTO() { Id = 2, Name = "Haarklippere", ParentCategory = new CategoryDTO() { Id = 0, Name = "Barbering" } }
               },
               new ProductDTO()
               {
                   Id = 4, Name = "Remington Apprentice", Price = 149.00, PointGain = 15, Description = "Remington Apprentice", ImageSource = "ProductList_200x150 (2).png", Category = new CategoryDTO() { Id = 3, Name = "Skrabere", ParentCategory = new CategoryDTO() { Id = 0, Name = "Barbering" } }
               },
               new ProductDTO()
               {
                   Id = 5, Name = "Apprentice", Price = 149.00, PointGain = 15, Description = "Apprentice", ImageSource = "ProductList_200x150 (2).png", Category = new CategoryDTO() { Id = 4, Name = "Gaveaesker", ParentCategory = new CategoryDTO() { Id = 1, Name = "Dufte"} }
               }
            };

            _sessionContainer = new SessionContainer();
        }



        public IActionResult Index()
        {
            //var tempCategories = _productService.GetAllCategories();
            var tempProducts = _productService.GetAllProducts();
            var model = new ProductViewModel();
            model.ActiveCategory = "index";
            foreach (var product in tempProducts)
            {
                if(!model.SubCategoryList.Select(x => x.Id).Contains(product.Category.Id) && product.Category.ParentCategory != null)
                {
                    if(!model.MainCategoryList.Select(x => x.Id).Contains(product.Category.ParentCategory.Id) && product.Category.ParentCategory.ParentCategory == null)
                    {
                        model.MainCategoryList.Add(product.Category.ParentCategory);
                    }
                    model.SubCategoryList.Add(product.Category);
                }
                if (!model.MainCategoryList.Select(x => x.Id).Contains(product.Category.Id) && product.Category.ParentCategory == null)
                {
                    model.MainCategoryList.Add(product.Category);
                }
            }
            return View(model);
        }

        public IActionResult MainCategory(string mainCategory, string subCategory, string subsubCategory)
        {
            var tempProducts = _productService.GetAllProducts();
            var model = new ProductViewModel();
            model.ActiveCategory = mainCategory;
            foreach (var product in tempProducts)
            {
                if (!model.SubCategoryList.Select(x => x.Id).Contains(product.Category.Id) && product.Category.ParentCategory != null)
                {
                    if (!model.MainCategoryList.Select(x => x.Id).Contains(product.Category.ParentCategory.Id) && product.Category.ParentCategory.ParentCategory == null)
                    {
                        model.MainCategoryList.Add(product.Category.ParentCategory);
                        model.ProductText = product.Category.ParentCategory.ProductText;
                    }
                    model.SubCategoryList.Add(product.Category);
                }
                if (!model.MainCategoryList.Select(x => x.Id).Contains(product.Category.Id) && product.Category.ParentCategory == null)
                {
                    model.MainCategoryList.Add(product.Category);
                }
                if (model.ProductText != "" && product.Category.Name == model.ActiveCategory)
                {
                    model.ProductText = product.Category.ProductText;
                }

            }
            if (subCategory != null)
            {
                if(subsubCategory != null)
                {
                    foreach (var product in tempProducts)
                    {
                        if (product.Category.Name == subsubCategory && product.Category.ParentCategory != null && product.Category.ParentCategory.ParentCategory != null)
                        {
                            model.ProductList.Add(product);
                        }

                    }
                }
                else
                {
                    foreach (var product in tempProducts)
                    {
                        if (product.Category.Name == subCategory && product.Category.ParentCategory != null)
                        {
                            model.ProductList.Add(product);
                        }

                    }
                }

            }
            else
            {
                foreach (var product in tempProducts)
                {
                    if ((product.Category.ParentCategory != null && product.Category.ParentCategory.Name == mainCategory) || product.Category.Name == mainCategory && !model.ProductList.Select(x => x.Id).Contains(product.Id))
                    {
                        model.ProductList.Add(product);
                    }
                }

            }
            //Skal fås fra db
            //model.ProductText = "Barberkost og kniv, skraber og gelé – en kær barbering har mange faconer. Uanset om du er til retro-metoden eller mere moderne værktøjer, er det vigtigt, at du bruger de rigtige redskaber. En god barbering er nemlig et must for en hver mand.Et skarpt blad, en fugtet hud og et blødgjort skæg er essentiel for din skægpleje. På matas.dk finder du flere forskellige produkter til skægpleje; fx skægbalsam, skægolie, barberblade, trimmere m.m. En god skægpleje holder dit skæg i god form. Læs også artiklen barberingsguide og få et perfekt skæg.";

            return View("index", model);
        }

        [HttpPost]
        public int AddToBasket([FromBody] int productId) {
            _sessionContainer.AddToSession(HttpContext, "basket", productId);

            return _sessionContainer.BasketCount(HttpContext, "basket");
        }

        [HttpPost]
        public int RemoveFromBasket([FromBody] int id) {
            _sessionContainer.RemoveFromSession(HttpContext, "basket", id);
            return _sessionContainer.BasketCount(HttpContext, "basket");
        }

        [HttpPost]
        public int RemoveAllFromBasket([FromBody] int id) {
            _sessionContainer.RemoveAllFromSession(HttpContext, "basket", id);
            return _sessionContainer.BasketCount(HttpContext, "basket");
        }

        [HttpPost]
        public int BasketCount() {
            return _sessionContainer.BasketCount(HttpContext, "basket");
        }
    }
}
