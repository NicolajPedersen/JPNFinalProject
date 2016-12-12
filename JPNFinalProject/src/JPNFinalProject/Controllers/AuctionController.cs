using JPNFinalProject.Models.AuctionViewModels;
using JPNFinalProject.Services.DatabaseServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Controllers
{
    public class AuctionController : Controller
    {
        private ProductService _productService;
        public AuctionController()
        {
            _productService = new ProductService();
        }

        public IActionResult Index()
        {
            var model = new AuctionViewModel();

            model.Product = _productService.GetRandomProduct();


            return View(model);
        }

        public IActionResult Client()
        {
            return View();
        }

    }
}
