using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace JPNFinalProject.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            string name = Context.ConnectionId;
            Clients.All.onMessageReceived(DateTime.Now.ToString(), name, message);
        }

        public override Task OnConnected()
        {
            string name = Context.ConnectionId;
            Groups.Add(Context.ConnectionId, name);
            return base.OnConnected();
        }
    }
}