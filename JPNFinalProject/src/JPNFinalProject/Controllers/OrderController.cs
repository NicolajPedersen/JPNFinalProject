using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.OrderViewModels;
using JPNFinalProject.Data.DTO;

namespace JPNFinalProject.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Basket() {
            BasketViewModel model = new BasketViewModel();
            model.Products = new List<ProductDTO>() {
                new ProductDTO () {Name = "Remington Apprentice Hårklipper", Price = 199.00, ImageSource = "ProductList_200x150 (2).png" },
                new ProductDTO () {Name = "Matas Natur Havtorn & Blåbær Shampoo", Price = 74.95, ImageSource = "ProductPage_350x262.png" },
            };
            return View(model);
        }

        public IActionResult Delivery()
        {
            return View();
        }

        public IActionResult Overview() {
            return View();
        }
    }
}