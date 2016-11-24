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

        [HttpGet]
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
        [HttpPost]
        public IActionResult Delivery(DeliveryViewModel dModel)
        {
            var delivery = dModel;
            return RedirectToAction("Overview", delivery);
        }

        [HttpGet]
        public IActionResult Overview(DeliveryViewModel delivery) {
            OverviewViewModel model = new OverviewViewModel();

            if (_sessionContainer.BasketCount(HttpContext, "basket") != 0) {
                model.Order = new OrderDTO() {
                    Id = 1,
                    OrderNumber = 12345,
                    CustomerMail = "",
                    Person = new PersonDTO() {
                        Id = 1,
                        FirstName = delivery.Name,
                        LastName = delivery.Name,
                        Email = delivery.Email,
                        Password = "",
                        Phone = delivery.Phone,
                        Type = "",
                        Address = new AddressDTO() {
                            Id = 1,
                            Address = delivery.Street,
                            ZipCode = delivery.Zip,
                            City = delivery.City,
                            Country = "Test Country"
                        }
                    },
                    Business = new BusinessDTO() {
                        Id = 1,
                        Name = "Matas",
                        Address = new AddressDTO() {
                            Id = 2,
                            Address = "Test 2",
                            ZipCode = "2222",
                            City = "Test City",
                            Country = "Test Country"
                        },
                        Phone = "23456789",
                        Email = "test2@test2.com",
                        OperationalHour = ""
                    }
                };
                model.Order.Products = _sessionContainer.GetBasket(HttpContext, "basket");
            }
            else {
                model.Order = new OrderDTO() {
                    Person = new PersonDTO() { Address = new AddressDTO() },
                    Business = new BusinessDTO() { Address = new AddressDTO() }
                };
                model.Order.Products = new List<ProductDTO>();
            }

            model.Subtotal = model.Order.Products.Select(x => x.Price).Sum();
            model.VATFromPrice = (model.Subtotal / 100) * 25;
            model.PriceWithVAT = model.Subtotal + model.VATFromPrice;
            model.Order.Products = new List<ProductDTO>();

            return View(model);
        }

        [HttpGet]
        public IActionResult PaymentDone() {
            PaymentDoneViewModel model = new PaymentDoneViewModel();
            model.Order = new OrderDTO() {
                Id = 1,
                OrderNumber = 12345,
                Person = new PersonDTO() {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "test1@test1.com",
                    Password = "",
                    Phone = "21345678",
                    Type = "",
                    Address = new AddressDTO() {
                        Id = 1,
                        Address = "Test 1",
                        ZipCode = "1111",
                        City = "Test City",
                        Country = "Test Country"
                    }
                },
                Business = new BusinessDTO() {
                    Id = 1,
                    Name = "Matas",
                    Address = new AddressDTO() {
                        Id = 2,
                        Address = "Test 2",
                        ZipCode = "2222",
                        City = "Test City",
                        Country = "Test Country"
                    },
                    Phone = "23456789",
                    Email = "test2@test2.com",
                    OperationalHour = ""
                }
            };

            model.Order.Products = ProductController.productList.Take(2).ToList();
            model.Order.Products.ForEach(x => x.Amount = 1);

            model.Subtotal = model.Order.Products.Select(x => x.Price).Sum();
            model.VATFromPrice = (model.Subtotal / 100) * 25;
            model.PriceWithVAT = model.Subtotal + model.VATFromPrice;

            return View(model);
        }
        //[HttpGet]
        //public IActionResult Payment(int id)
        //{
        //    //var x = new PaymentViewModel() { }
        //    //var order = ´service.getorder(id)
        //    //get stuff from db


        //    return View("Payment", x);
        //}
    }
}