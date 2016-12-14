using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using JPNFinalProject.Hubs;

namespace JPNFinalProject.Controllers
{
    public class ChatController : Controller
    {
        public IHubContext<ChatHub> ChatHubContext { get; private set; }

        public ChatController(IHubContext<ChatHub> chatHubContext)
        {
            ChatHubContext = chatHubContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Poke()
        {
            ChatHubContext.Clients.All.poke("Hey!");
            return Json(null);
        }
    }
}
