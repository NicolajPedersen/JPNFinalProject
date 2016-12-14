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
        public bool IsConnected { get; set; }
    }


    [HubName("test")]
    public class TestHub : Hub
    {
        public static ConcurrentDictionary<string, Storage> userL = new ConcurrentDictionary<string, Storage>();

        //public void SignalRConnectionId(string user, int businessId) {
        //    var signalRConnectionId = this.Context.ConnectionId;
        //    if (user == "Admin") {
        //        userL.TryAdd(signalRConnectionId, new Storage() { User = user, BusinessId = businessId });
        //    }
        //    Clients.Client(signalRConnectionId).getConnectionId($"{signalRConnectionId}");
        //}

        public void SignalRConnectionIdV2(string connectionId) {
            Clients.Client(connectionId).getConnectionId($"{connectionId}");
        }

        public void AddAdmin(string connectionId, string user, int businessId) {
            userL[connectionId] = new Storage() { User = user, BusinessId = businessId };
        }

        public void AddObject(string connectionId, Storage input) {
            input.IsConnected = true;
            //userL.TryAdd(connectionId, input);

            userL[connectionId] = input;

            var adminId = userL.Where(x => x.Value.User == "Admin").Select(x => x.Key).Single();
            var l = userL.Where(x => x.Value.User == "User");
            Clients.Client(adminId).getAll(l);
        }

        public void SendMessage(string id, string message) {
            Clients.Client(id).getMessage(message);
        }

        //Skal lige huske at have lavet sådan at når man disconnecter så bliver man fjernet fra listen.

        public override Task OnConnected() {
            string connectionId = this.Context.ConnectionId;
            userL.TryAdd(connectionId, new Storage());

            SignalRConnectionIdV2(connectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            //string connectionId = Context.ConnectionId;
            //Storage value;
            //userL.TryRemove(connectionId, out value);

            string connectionId = Context.ConnectionId;
            userL[connectionId].IsConnected = false;

            var adminId = userL.Where(x => x.Value.User == "Admin").Select(x => x.Key).Single();
            var l = userL.Where(x => x.Value.User == "User");
            //Clients.Client(adminId).getAll(l);

            return base.OnDisconnected(stopCalled);
        }

        //public override Task OnReconnected() { //http://stackoverflow.com/questions/22002092/context-user-identity-name-is-null-with-signalr-2-x-x-how-to-fix-it/22028296#22028296
        //    string name = Context.User.Identity.Name;

        //    if (!_connections.GetConnections(name).Contains(Context.ConnectionId)) {
        //        _connections.Add(name, Context.ConnectionId);
        //    }

        //    return base.OnReconnected();
        //}
    }
}
