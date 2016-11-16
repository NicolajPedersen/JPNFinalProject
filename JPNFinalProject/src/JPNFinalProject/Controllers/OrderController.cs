using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.OrderViewModels;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services;
using Microsoft.AspNetCore.Http;

namespace JPNFinalProject.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Basket() {
            //SessionContainer sessionContainer = new SessionContainer();
            BasketViewModel model = new BasketViewModel();
            model.Products = new List<ProductDTO>();
            //var productIds = sessionContainer.GetBasket("basket");
            var productIds = HttpContext.Session.GetString("basket");
            //foreach (var i in HttpContext.Session.Keys) {
            //    if (i == "basket") {
            //        HttpContext.Session.GetString(i);
            //        productIds.Add(Convert.ToInt32(item));
            //    }
            //}
            foreach (var p in ProductController.productList) {
                foreach(var id in productIds)
                if (p.Id == id) {
                        p.Amount = 1;
                        model.Products.Add(p);
                    }
            }
            //model.Products = new List<ProductDTO>() {
            //    new ProductDTO () {Name = "Remington Apprentice Hårklipper", Price = 199.00, Amount = 1, ImageSource = "ProductList_200x150 (2).png" },
            //    new ProductDTO () {Name = "Matas Natur Havtorn & Blåbær Shampoo", Price = 75, Amount = 1, ImageSource = "ProductPage_350x262.png" },
            //};
            return View(model);
        }

        public IActionResult Delivery()
        {
            return View();
        }

        public IActionResult Overview() {
            OverviewViewModel model = new OverviewViewModel();

            return View();
        }
    }
}

//public static class SessionExtensions {
//    public static void SetObjectAsJson(this ISession session, string key, object value) {
//        session.SetString(key, JsonConvert.SerializeObject(value));
//    }

//    public static T GetObjectFromJson<T>(this ISession session, string key) {
//        var value = session.GetString(key);

//        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
//    }
//}