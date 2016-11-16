using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.OrderViewModels;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JPNFinalProject.Controllers
{
    public class OrderController : Controller
    {
        private SessionContainer _sessionContainer;
        public OrderController() {
            _sessionContainer = new SessionContainer();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Basket() {
            BasketViewModel model = new BasketViewModel();
            model.Products = new List<ProductDTO>();
            var productIds = _sessionContainer.GetBasket(HttpContext, "basket");
            if (productIds != null) {
                foreach (var p in ProductController.productList) {
                    foreach (var id in productIds)
                        if (p.Id == id) {
                            p.Amount = 1;
                            model.Products.Add(p);
                        }
                }
            }
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