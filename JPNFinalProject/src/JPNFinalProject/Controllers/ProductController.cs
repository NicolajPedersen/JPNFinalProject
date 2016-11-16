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

        public ProductController()
        {
            productList = new List<ProductDTO>()
            {
               new ProductDTO()
               {
                   Id = 1, Name = "Remington HC5600 E51 Pro Power Hair Clipper", Price = 300, PointGain = 30, Description = "Remington HC5600 E51 Pro Power Hair Clipper", ImageSource = "ProductList_200x150.png"
               },
               new ProductDTO()
               {
                   Id = 2, Name = "Remington Pro Power Hårklipper HC5200", Price = 249, PointGain = 25, Amount = 0, Description = "Remington Pro Power Hårklipper HC5200", ImageSource = "ProductList_200x150 (1).png"
               },
               new ProductDTO()
               {
                   Id = 3, Name = "Remington Apprentice Hårklipper", Price = 199, PointGain = 20, Description = "Remington Apprentice Hårklipper", ImageSource = "ProductList_200x150 (2).png"
               }
            };
        }



        public IActionResult Index()
        {
            var model = new ProductViewModel();
            model.ProductList = productList;
            return View(model);
        }

        //[HttpPost]
        //public void AddToBasket([FromBody] int productId)
        //{
        //    SessionContainer sessionContainer = new SessionContainer();

        //    sessionContainer.AddToSession("test1", productId.ToString());
        //}

        [HttpPost]
        public void AddToBasket([FromBody] int productId) {
            //SessionContainer sessionContainer = new SessionContainer();
            //HttpContextAccessor accessor = new HttpContextAccessor();
            //sessionContainer.AddToSession("basket", productId.ToString());
            var item = HttpContext.Session.GetString("basket");
            if (HttpContext.Session.GetString("basket") != null) {
                List<int> basket1 = JsonConvert.DeserializeObject(item);
                HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(basket));
            }
            List<int> basket = new List<int>();
            basket.Add(productId);
            
            HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(basket));
        }
    }
}

//public static class SessionExtensions {
//    static SessionContainer sessionContainer = new SessionContainer();
//    public static void SetObject(ISession session, string key, string value) {
//        sessionContainer.AddToSession(session, key, value);
//    }

//    public static List<int> GetBasket(string key) {
//        return sessionContainer.GetBasket(key);
//    }
//}
