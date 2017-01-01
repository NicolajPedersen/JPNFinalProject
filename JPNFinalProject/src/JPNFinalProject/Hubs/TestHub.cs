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
using JPNFinalProject.Services.DatabaseServices;

namespace JPNFinalProject.Hubs
{
    public class Storage {
        public string User { get; set; }
        public int BusinessId { get; set; }
        public int OrderId { get; set; }
        public bool IsConnected { get; set; }
        public bool? OrderConfirmed { get; set; }
    }


    [HubName("test")]
    public class TestHub : Hub
    {
        public static ConcurrentDictionary<string, Storage> userL = new ConcurrentDictionary<string, Storage>();
        private OrderService _orderService = new OrderService();
        private ProductService _productService = new ProductService();

        public void SignalRConnectionIdV2(string connectionId) {
            Clients.Client(connectionId).getConnectionId($"{connectionId}");
        }

        public void AddAdmin(string connectionId, string user, int businessId) {
            userL[connectionId] = new Storage() { User = user, BusinessId = businessId };

            var l = userL.Where(x => x.Value.User == "User");
            Clients.Client(connectionId).getAll(l);
        }

        public void AddObject(string connectionId, Storage input) {
            try {
                var adminId = userL.Where(x => x.Value.User == "Admin").Select(x => x.Key).Single();
                input.IsConnected = true;
                userL[connectionId] = input;
                var l = userL.Where(x => x.Value.User == "User");
                Clients.Client(adminId).getAll(l);
            }
            catch {
                input.IsConnected = true;
                userL[connectionId] = input;
            }
        }

        //public void SendMessage(string id, string message) {
        //    Clients.Client(id).getMessage(message);
        //}

        //public void SendMessage2(string id, int productId, string message) {
        //    var productName = _productService.GetProductById(productId).Name;

        //    Clients.Client(id).getMessage(new { productId, productName, message });
        //}

        public void SendMessage3(string id, List<Object> info) {
            Clients.Client(id).getMessage(info);
        }

        public void SendMessageToAdmin(string connectionId, bool orderConfirmed, List<int> removeProducts) {
            try {
                var adminId = userL.Where(x => x.Value.User == "Admin").Select(x => x.Key).Single();
                userL[connectionId].OrderConfirmed = orderConfirmed;
                var orderId = userL[connectionId].OrderId;

                if (orderConfirmed == true && removeProducts.Any() == true) {
                    //Her skal alle produkterne i listen så fjernes fra ordren.
                    //Eller de kunne blive flyttet over i en anden order, hvor kunden så kunne få lov til at finde en anden butik hvor de var i.
                    _orderService.DeleteProductsFromOrder(orderId, removeProducts);
                }

                if (orderConfirmed == false) {
                    _orderService.DeleteOrder(orderId);
                }

                var l = userL.Where(x => x.Value.User == "User");
                Clients.Client(adminId).getAll(l);
            }
            catch {
                userL[connectionId].OrderConfirmed = orderConfirmed;
            }
        }

        //Skal lige huske at have lavet sådan at når man disconnecter så bliver man fjernet fra listen.

        public override Task OnConnected() {
            string connectionId = this.Context.ConnectionId;
            userL.TryAdd(connectionId, new Storage());

            SignalRConnectionIdV2(connectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled) {
            string connectionId = Context.ConnectionId;
            userL[connectionId].IsConnected = false;

            var adminId = userL.Where(x => x.Value.User == "Admin").Select(x => x.Key).Single();
            var l = userL.Where(x => x.Value.User == "User");
            Clients.Client(adminId).getAll(l);

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
