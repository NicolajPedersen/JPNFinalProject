using JPNFinalProject.Models.EmployeeViewModels;
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
        public void Hello()
        {
            this.Clients.All.sayHello($"Hello {Context.ConnectionId}");
            
        }

        public void GetConnectionId()
        {
            this.Clients.All.sayId($"{Context.ConnectionId}");
        }

        public void GetOrder(string id)
        {
            this.Clients.User(id).updateList($"Test");
        }
    }
}
