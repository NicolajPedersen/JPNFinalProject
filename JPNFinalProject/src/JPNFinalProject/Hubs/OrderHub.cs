using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Hubs
{
    [HubName("order")]
    public class OrderHub : Hub
    {
        public void Hello(string hhh)
        {
            Clients.All.sayHello($"Hello {hhh}");
            
        }
    }
}
