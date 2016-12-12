using JPNFinalProject.Services.DatabaseServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Hubs
{
    [HubName("auction")]
    public class AuctionHub : Hub
    {

        public void StartAuction(string productId)
        {
            ProductService _productService = new ProductService();

            var product = _productService.GetProductById(Convert.ToInt32(productId));

            Clients.All.auctionStarted("Auktionen på produktet: " + product.Name + " er startet");
        }

        //public void SignalRConnectionId(string user, int businessId)
        //{
        //    var signalRConnectionId = this.Context.ConnectionId;
        //    if (user == "Admin")
        //    {
        //        userL.TryAdd(signalRConnectionId, new Storage() { User = user, BusinessId = businessId });
        //    }
        //    Clients.Client(signalRConnectionId).getConnectionId($"{signalRConnectionId}");
        //}

    }
}
