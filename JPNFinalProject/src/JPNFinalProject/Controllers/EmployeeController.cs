using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.ProductViewModels;
using JPNFinalProject.Models.EmployeeViewModels;
using JPNFinalProject.Data.DTO;

namespace JPNFinalProject.Controllers
{
    public class EmployeeController : Controller
    {
        List<OrderDTO> productList;
        public EmployeeController()
        {
            productList = new List<OrderDTO>()
            {
               new OrderDTO()
               {
                   OrderNumber = 24553892, CustomerMail = "phvorret@gmail.com" , Products = new List<ProductDTO>()
                                                                                {
                                                                                    new ProductDTO()
                                                                                    {
                                                                                        Name = "Remington HC5600 E51 Pro Power Hair Clipper",
                                                                                        Price = 300,
                                                                                        Amount = 1,
                                                                                        StockAmount = 50,
                                                                                        PutAside = true,
                                                                                        Description = "Remington HC5600 E51 Pro Power Hair Clipper"
                                                                                    },
                                                                                    new ProductDTO()
                                                                                    {
                                                                                        Name = "Remington Pro Power Hårklipper HC5200",
                                                                                        Price = 249,
                                                                                        Amount = 1,
                                                                                        StockAmount = 0,
                                                                                        PutAside = false,
                                                                                        Description = "Remington Pro Power Hårklipper HC5200"
                                                                                    }
                                                                                }

               },
               new OrderDTO()
               {
                   OrderNumber = 26836478, CustomerMail = "ngp@gmail.com" , Products = new List<ProductDTO>()
                                                                            {
                                                                                new ProductDTO()
                                                                                {
                                                                                    Name = "Remington Apprentice Hårklipper",
                                                                                    Price = 199,
                                                                                    Amount = 1,
                                                                                    StockAmount = 10,
                                                                                    PutAside = false,
                                                                                    Description = "Remington Apprentice Hårklipper"
                                                                                }
                                                                            }
               }
            };
        }

        public IActionResult Index()
        {
            var model = new EmployeeViewModel();
            model.OrderList = productList;
            return View(model);
        }
    }
}