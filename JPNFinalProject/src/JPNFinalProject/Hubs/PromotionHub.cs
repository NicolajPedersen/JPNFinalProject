using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace JPNFinalProject.Hubs
{
    [HubName("promotion")]
    public class PromotionHub : Hub
    {
        private static ConcurrentDictionary<string, string> _clients = new ConcurrentDictionary<string, string>();

        public PromotionHub()
        {
            if (true)
            {

            }
        }
        //public void SendPromotion(string message)
        //{
        //    string querytype = Context.QueryString["transport"];
        //    string name = Context.ConnectionId;
        //    Clients.Others.onMessageReceived(message);
        //}
        public void SendPromotion(Message message)
        {          
            Clients.Group(message.Group).onMessageReceived(message.Msg);
        }

        public override Task OnConnected()
        {
            string name = Context.ConnectionId;
            Groups.Add(Context.ConnectionId, name);
            return base.OnConnected();
        }
        public void JoinGroup(string groupName)
        {
            this.Groups.Add(this.Context.ConnectionId, groupName);
            Clients.Group(groupName).onMessageReceived(groupName);
        }
    }
    public class Message
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }
        [JsonProperty("group")]
        public string Group { get; set; }
    }
}