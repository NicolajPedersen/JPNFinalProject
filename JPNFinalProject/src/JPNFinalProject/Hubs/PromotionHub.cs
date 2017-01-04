using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace JPNFinalProject.Hubs
{
    [HubName("promotion")]
    public class PromotionHub : Hub
    {
        public void SendPromotion(string message)
        {
            string querytype = Context.QueryString["transport"];
            string name = Context.ConnectionId;
            Clients.Others.onMessageReceived(message);
        }

        public override Task OnConnected()
        {
            string name = Context.ConnectionId;
            Groups.Add(Context.ConnectionId, name);
            return base.OnConnected();
        }
    }
}