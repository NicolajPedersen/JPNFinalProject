using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.ProductViewModels;

namespace JPNFinalProject.Controllers
{
    public class ProductController : Controller
    {
        List<ProductViewModel> productList;

        public ProductController()
        {
            productList = new List<ProductViewModel>()
            {
               new ProductViewModel()
               {
                   Id = 1 ,Name = "Remington HC5600 E51 Pro Power Hair Clipper", Price = 300, PointGain = 30, Description = "Remington HC5600 E51 Pro Power Hair Clipper", ImageSource = "ProductList_200x150.png"
               },
               new ProductViewModel()
               {
                   Id = 2 ,Name = "Remington Pro Power Hårklipper HC5200", Price = 249, PointGain = 25, Description = "Remington Pro Power Hårklipper HC5200", ImageSource = "ProductList_200x150 (1).png"
               },
               new ProductViewModel()
               {
                   Id = 3 ,Name = "Remington Apprentice Hårklipper", Price = 199, PointGain = 20, Description = "Remington Apprentice Hårklipper", ImageSource = "ProductList_200x150 (2).png"
               }
            };
        }



        public IActionResult Index()
        {
            var model = new ProductViewModel();
            model.ProductList = productList;
            return View(model);
        }
    }
}
