﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Services.DatabaseServices;

namespace JPNFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private ProductService _productService;

        public HomeController()
        {
            _productService = new ProductService();
            Data.SeedData.Seed();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public void AddProduct()
        {
            _productService.AddRandomProduct();
        }
        [HttpPost]
        public void AddRandomCategory()
        {
            _productService.AddRandomCategory();
        }
    }
}
