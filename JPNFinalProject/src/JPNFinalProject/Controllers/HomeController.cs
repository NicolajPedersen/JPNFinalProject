using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Data.DatabaseModels;

namespace JPNFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private JPNFinalProjectContext _dbContext;
        public HomeController(JPNFinalProjectContext context) {
            _dbContext = context;
            
        }
        public IActionResult Index()
        {
            _dbContext.Address.Add(new Address() { Street = "FFF", PostalCode = "FF", City = "FFFFF", Country = "fffff" });
            _dbContext.SaveChanges();
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
    }
}
