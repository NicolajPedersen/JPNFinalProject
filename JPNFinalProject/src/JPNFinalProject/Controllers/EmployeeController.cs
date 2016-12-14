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
            //Her kalder vi metoden der går ud i DB og opdater ordre hvis den ikke er på lager. Og metoden som kontakter kunden med info.
        }

        [HttpPost]
        public void PutAside([FromBody] List<int> product)
        {
            //Her kalder vi metoden som går ud og sætter produktet som sat tilside og fortæller kunden

            var orderdto = _orderService.GetOrderByOrderNumber(product[1]);

            var email = orderdto.CustomerMail;  

            SendMail(email, orderdto);
        }

        private void SendMail(string email, OrderDTO order)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("JPN", "JPNFinalProject@gmail.com"));
            message.To.Add(new MailboxAddress(order.Person.Name, email));

            message.Subject = "Hello " + order.Person.Name;

            message.Body = new TextPart("plain")
            {
                Text = "Hi " + order.Person.Name + "\n\nDu har købt: " + order.Products[0].Name 
            };

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