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

            if (_sessionContainer.GetBasket(HttpContext, "basket") != null) {
                model.Products = _sessionContainer.GetBasket(HttpContext, "basket");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delivery() {
            return View();
        }

        public IActionResult Overview() {
            return View();
        }
    }
}