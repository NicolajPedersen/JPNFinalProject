using JPNFinalProject.Data.DTO;
using JPNFinalProject.Models.EmployeeViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Hubs
{
    [HubName("order")]
    public class OrderHub : Hub
    {

        public static ConcurrentDictionary<int, string> employee = new ConcurrentDictionary<int, string>();

        public void Hello()
        {
            this.Clients.All.sayHello($"Hello {Context.ConnectionId}");
            
        }

        public void GetConnectionId(int id)
        {
            employee.TryAdd(id, Context.ConnectionId);
            var tempValue = employee[id];
            employee.TryUpdate(id, Context.ConnectionId, tempValue);
            this.Clients.All.sayId($"{Context.ConnectionId} + {employee.Count}");
        }

        public void GetOrder()
        {
            var id = employee[2];
            this.Clients.Client(id).updateList(new OrderDTO());
        }

        public void GetNewOrder(OrderDTO order)
        {
            var connectionID = employee[order.Business.Id];
            this.Clients.Client(connectionID).updateOrders(order);
        }
    }
}
