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

namespace JPNFinalProject.Controllers
{
    public class ProductController : Controller
    {
        public static List<ProductDTO> productList;
        private SessionContainer _sessionContainer;

        public ProductController()
        {
            productList = new List<ProductDTO>()
            {
               new ProductDTO()
               {
                   Id = 1, Name = "Remington HC5600 E51 Pro Power Hair Clipper", Price = 300.00, PointGain = 30, Description = "Remington HC5600 E51 Pro Power Hair Clipper", ImageSource = "ProductList_200x150.png"
               },
               new ProductDTO()
               {
                   Id = 2, Name = "Remington Pro Power Hårklipper HC5200", Price = 249.00, PointGain = 25, Amount = 0, Description = "Remington Pro Power Hårklipper HC5200", ImageSource = "ProductList_200x150 (1).png"
               },
               new ProductDTO()
               {
                   Id = 3, Name = "Remington Apprentice Hårklipper", Price = 199.00, PointGain = 20, Description = "Remington Apprentice Hårklipper", ImageSource = "ProductList_200x150 (2).png"
               }
            };

            _sessionContainer = new SessionContainer();
        }



        public IActionResult Index()
        {
            var model = new ProductViewModel();
            model.ProductList = productList;
            return View(model);
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
        public int BasketCount() {
            return _sessionContainer.BasketCount(HttpContext, "basket");
        }
    }
}
