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
        public void SignalRConnectionId(string signalRConnectionId) {
            Clients.Client(signalRConnectionId).getConnectionId($"{Context.ConnectionId}");
        }

        public override Task OnConnected() {
            SignalRConnectionId(this.Context.ConnectionId);
            return base.OnConnected();
        }

        //public void Getlist(string id) {
        //    Clients.Client(id).getAll(TestHub.userL);
        //}
    }
}
