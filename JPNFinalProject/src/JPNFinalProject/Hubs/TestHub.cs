using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services;
using Microsoft.AspNetCore.Http;

namespace JPNFinalProject.Hubs
{
    public class Storage {
        public string User { get; set; }
        public int BusinessId { get; set; }
        public int OrderId { get; set; }
    }


    [HubName("test")]
    public class TestHub : Hub
    {
        public static ConcurrentDictionary<string, Storage> userL = new ConcurrentDictionary<string, Storage>();

        public void SignalRConnectionId(string user, int businessId) {
            var signalRConnectionId = this.Context.ConnectionId;
            if (user == "Admin") {
                userL.TryAdd(signalRConnectionId, new Storage() { User = user, BusinessId = businessId });
            }
            Clients.Client(signalRConnectionId).getConnectionId($"{signalRConnectionId}");
        }

        public void AddObject(string connectionId, Storage input) {
            userL.TryAdd(connectionId, input);

            var adminId = userL.Where(x => x.Value.User == "Admin").Select(x => x.Key).Single();
            var l = userL.Where(x => x.Value.User == "User");
            Clients.Client(adminId).getAll(l);
        }

        public void SendMessage(string id, string message) {
            Clients.Client(id).getMessage(message);
        }

        //Skal lige huske at have lavet sådan at når man disconnecter så bliver man fjernet fra listen.

        //public override Task OnConnected() {
        //    SignalRConnectionId(this.Context.ConnectionId);
        //    return base.OnConnected();
        //}

        public override Task OnDisconnected(bool stopCalled) {
            string connectionId = Context.ConnectionId;
            Storage value;
            userL.TryRemove(connectionId, out value);

            return base.OnDisconnected(stopCalled);
        }

        //public override Task OnReconnected() {
        //    string name = Context.User.Identity.Name;

        //    if (!_connections.GetConnections(name).Contains(Context.ConnectionId)) {
        //        _connections.Add(name, Context.ConnectionId);
        //    }

        //    return base.OnReconnected();
        //}
    }
}
