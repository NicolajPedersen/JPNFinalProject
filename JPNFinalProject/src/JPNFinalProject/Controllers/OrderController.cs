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
using JPNFinalProject.Services.DatabaseServices;
using JPNFinalProject.Models.ViewModelsMappers;

namespace JPNFinalProject.Controllers
{
    public class OrderController : Controller
    {
        //https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db //Sådan benytter du dig af Entity Framework - Existing Database
        private SessionContainer _sessionContainer;
        private OrderService _orderService;

        public OrderController() {
            _sessionContainer = new SessionContainer();
            _orderService = new OrderService();
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
            //delivery.Businesses = _orderService.GetBusinesses();
            return RedirectToAction("Overview", delivery); //Skal det ikke bare ligges til session, sådan at det ikke skal sendes som parameter, eventuelt order.
        }

        [HttpGet]
        public IActionResult Overview(DeliveryViewModel delivery) {
            if (_sessionContainer.BasketCount(HttpContext, "basket") != 0) {
                var model = OverviewViewModelMapper.DeliveryViewModelToOverviewViewModel(delivery);
                model.Order.Business = _orderService.GetBusinessById(Convert.ToInt32(delivery.ParcelPickup));
                model.Order.Products = _sessionContainer.GetBasket(HttpContext, "basket");

                model.Subtotal = model.Order.Products.Select(x => x.Price * x.Amount).Sum();
                model.VATFromPrice = (model.Subtotal / 100) * 25;
                model.Order.TotalPrice = model.Subtotal + model.VATFromPrice;

                _sessionContainer.AddOrderToSession(HttpContext, "order", model.Order);

                return View(model);
            }
            else {
                OverviewViewModel model = new OverviewViewModel();
                model.Order = new OrderDTO() {
                    Person = new PersonDTO() { Address = new AddressDTO() },
                    Business = new BusinessDTO() { Address = new AddressDTO() }
                };
                model.Order.Products = new List<ProductDTO>();
                return View(model);
            }
        }

        //[HttpGet]
        //public IActionResult Payment(int id) {
        //    PaymentViewModel model = new PaymentViewModel();
        //    var order = _sessionContainer.GetOrderFromSession(HttpContext, "order");
        //    var Subtotal = order.Products.Select(x => x.Price).Sum();
        //    var VATFromPrice = (Subtotal / 100) * 25;
        //    var PriceWithVAT = Subtotal + VATFromPrice;

        //    model.OrderNumber = order.OrderNumber.ToString();
        //    model.TotalAmount = PriceWithVAT;

        //    return View(model);
        //}

        [HttpGet]
        public IActionResult PaymentDone() {
            //var orderId = _orderService.SaveOrder(HttpContext);
            PaymentDoneViewModel model = new PaymentDoneViewModel();
            /*model.Order = _orderService.GetOrderByOrderNumber(orderId);*/ //Skal der ikke være orderNumber databasen
            model.Order = _orderService.GetOrderByOrderNumber(1141);

            model.Subtotal = model.Order.Products.Select(x => x.Price * x.StockAmount).Sum();
            model.VATFromPrice = (model.Subtotal / 100) * 25;
            model.PriceWithVAT = model.Subtotal + model.VATFromPrice;

            return View(model);
        }

        [HttpPost]
        public IActionResult BusinessByPostalcode([FromBody] string postalcode)
        {
            var model = new BusinessViewModel();
            model.Businesses = _orderService.GetBusinesses(postalcode);
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult GetPersonByOrderId([FromBody] int[] orderIds) {
            return Json(_orderService.GetPersonsByOrderIds(orderIds.ToList()));
        }
    }
}