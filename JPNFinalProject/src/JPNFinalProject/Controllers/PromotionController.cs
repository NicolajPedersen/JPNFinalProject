using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using JPNFinalProject.Hubs;
using JPNFinalProject.Services.DatabaseServices;

namespace JPNFinalProject.Controllers
{
    public class PromotionController : Controller
    {
        private ProductService _productService;
        public IHubContext<PromotionHub> PromotionHubContext { get; private set; }

        public PromotionController(IHubContext<PromotionHub> promotionHubContext)
        {
            _productService = new ProductService();
            PromotionHubContext = promotionHubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Master()
        {
 
            var product = _productService.GetRandomProduct();
            return View(product);
           
        }
    }
}
