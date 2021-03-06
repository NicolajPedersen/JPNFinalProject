using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JPNFinalProject.Models.ProductViewModels;
using JPNFinalProject.Models.EmployeeViewModels;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services.DatabaseServices;
using MimeKit;
using MailKit.Net.Smtp;

namespace JPNFinalProject.Controllers
{
    public class EmployeeController : Controller
    {
        private string pass = "EAL2016JPN";
        private OrderService _orderService;
        public EmployeeController()
        {
            _orderService = new OrderService();           
        }

        public IActionResult Index()
        {
            //var tempList = _orderService.GetOrders();
            var model = new EmployeeViewModel();
            //model.BusinessId = tempList[0].Business.Id;
            //model.OrderList = tempList;
            return View(model);
        }

        [HttpPost]
        public ActionResult ProductPartial([FromBody] int orderId)
        {
            var model = new EmployeeViewModel();
            model.Order = _orderService.GetOrderByOrderNumberV2(orderId);
            return PartialView(model);
        }

        [HttpPost]
        public void NotInStock([FromBody] List<int> product)
        {
            //Her kalder vi metoden der g�r ud i DB og opdater ordre hvis den ikke er p� lager. Og metoden som kontakter kunden med info.

            var orderdto = _orderService.GetOrderByOrderNumber(product[1]);

            var email = orderdto.CustomerMail;

            var msg = "Hi " + orderdto.Person.Name + "\n\nDette produkt som du har pr�vet at bestille er ikke p� lager: " + orderdto.Products[0].Name;

            //SendMail(email, orderdto, msg);
        }

        [HttpPost]
        public void PutAside([FromBody] List<int> product)
        {
            //Her kalder vi metoden som g�r ud og s�tter produktet som sat tilside og fort�ller kunden

            var orderdto = _orderService.GetOrderByOrderNumber(product[1]);

            var email = orderdto.CustomerMail;

            var msg = "Hi " + orderdto.Person.Name + "\n\nDu har k�bt: " + orderdto.Products[0].Name;

            //SendMail(email, orderdto, msg);
        }

        private void SendMail(string email, OrderDTO order, string msg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("JPN", "JPNFinalProject@gmail.com"));
            message.To.Add(new MailboxAddress(order.Person.Name, email));

            message.Subject = "Hello " + order.Person.Name;

            message.Body = new TextPart("plain")
            {Text = msg};

            using(var client = new SmtpClient())
            {
                var uri = new Uri("smtps://smtp.gmail.com:465");

                client.Connect(uri);
                client.Authenticate("JPNFinalProject@gmail.com", pass);
                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}