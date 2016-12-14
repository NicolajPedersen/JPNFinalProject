using JPNFinalProject.Services.DatabaseServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Hubs
{
    [HubName("auction")]
    public class AuctionHub : Hub
    {
        public static ConcurrentDictionary<string, string> employees = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<int, double> productPrice = new ConcurrentDictionary<int, double>();

        public void StartAuction(string productId)
        {
            ProductService _productService = new ProductService();

            var product = _productService.GetProductById(Convert.ToInt32(productId));

            productPrice.TryAdd(product.Id, product.Price);

            Clients.All.auctionStarted("Auktionen på produktet: " + product.Name + " er startet. Start bud: " + product.Price, product.Id);
        }

        public void SignalRConnectionId(string user)
        {
            var signalRConnectionId = this.Context.ConnectionId;

            if (employees.ContainsKey(user))
            {
                employees[user] = signalRConnectionId;
            }
            else
            {
                employees.TryAdd(user, signalRConnectionId);
            }

            Clients.Client(employees[user]).getConnectionId(employees[user]);
        }

        public void UserConnectionId(string name)
        {
            var signalRConnectionId = this.Context.ConnectionId;

            if (users.ContainsKey(name))
            {
                users[name] = signalRConnectionId;
            }
            else
            {
                users.TryAdd(name, signalRConnectionId);
            }

            Clients.Client(users[name]).getUserConnectionId(users[name]);
        }

        public void MakeBid(string name, string productId, string bid)
        {
            var clientBid = Convert.ToDouble(bid);

            var proId = Convert.ToInt32(productId);

            var currentBid = productPrice[proId];

            bool isAccepted = false;

            string msg = "";

            if(clientBid > currentBid)
            {
                productPrice[proId] = clientBid;
                msg = "user: " + name + " Bid: " + clientBid + " is accepted";
                isAccepted = true;
            }
            else
            {
                msg = "user: " + name + " Bid:" + clientBid + " is not accepted";
                isAccepted = false;
            }

            Clients.Client(employees["employee"]).newBid(msg, clientBid, isAccepted, name);

        }

        public void NewAcceptedBid(string bid)
        {
            Clients.All.newAcceptBid("New bid accepted: " + bid);
        }

        public void NotAccepted(string user)
        {
            Clients.Client(users[user]).notAcceptedBid("Your bid was not accepted");
        }

    }
}
