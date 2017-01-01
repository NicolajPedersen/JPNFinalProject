using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JPNFinalProject.Controllers;
using JPNFinalProject.Data.DTO;
using JPNFinalProject.Services.DatabaseServices;

namespace JPNFinalProject.Services
{
    public class SessionContainer : Controller
    {
        /*
         * Add the Microsoft.AspNetCore.Session NuGet package to your project.
         * Open up startup.cs and add the AddSession() and AddDistributedMemoryCache() lines to the ConfigureServices(IServiceCollection services)
         * Add the UseSession() call below to the Configure(IApplicationBulider app, ...) // IMPORTANT: This session call MUST go before UseMvc()
        */
        private ProductService _productService;
        public SessionContainer() {
            _productService = new ProductService();
        }

        public void AddToSession(HttpContext context, string key, int value) {
            try {
                var item = context.Session.GetString(key);
                List<int> basket = JsonConvert.DeserializeObject<List<int>>(item);
                basket.Add(value);
                context.Session.SetString(key, JsonConvert.SerializeObject(basket));
            }
            catch {
                List<int> basket = new List<int>();
                basket.Add(value);
                context.Session.SetString(key, JsonConvert.SerializeObject(basket));
            }
        }

        public string GetSessionString(HttpContext context, string key) {
            return context.Session.GetString(key);
        }

        public List<ProductDTO> GetBasket(HttpContext context, string key) {
            try {
                var item = context.Session.GetString(key);
                List<int> productIds = JsonConvert.DeserializeObject<List<int>>(item);
                List<ProductDTO> products = new List<ProductDTO>();

                foreach (var p in _productService.GetAllProducts()) {
                    foreach (var id in productIds) {
                        if (p.Id == id && products.Select(x => x.Id).Contains(p.Id) == false) {
                            p.Amount = 1;
                            products.Add(p);
                        }
                        if (p.Id == id && products.Select(x => x.Id).Contains(p.Id) == true) {
                            var count = productIds.Where(x => x == id).Select(x => x).Count();
                            ProductDTO product = products.Where(x => x.Id == id).Select(x => x).Single();
                            product.Amount = count;
                        }
                    }
                }
                return products;
            }
            catch {
                return null;
            }
        }

        public int BasketCount(HttpContext context, string key) {
            try {
                var item = context.Session.GetString(key);
                List<int> productIds = JsonConvert.DeserializeObject<List<int>>(item);

                return productIds.Count();
            }
            catch {
                return 0;
            }
        }

        public void RemoveFromSession(HttpContext context, string key, int value) {
            try {
                var item = context.Session.GetString(key);
                List<int> productIds = JsonConvert.DeserializeObject<List<int>>(item);
                productIds.Remove(value);
                context.Session.SetString(key, JsonConvert.SerializeObject(productIds));
            }
            catch {

            }
        }

        public void RemoveAllFromSession(HttpContext context, string key, int value) {
            try {
                var item = context.Session.GetString(key);
                List<int> productIds = JsonConvert.DeserializeObject<List<int>>(item);
                foreach (var i in productIds.ToList()) {
                    if (i == value) {
                        productIds.Remove(i);
                    }
                }
                context.Session.SetString(key, JsonConvert.SerializeObject(productIds));
            }
            catch {

            }
        }

        public void AddOrderToSession(HttpContext context, string key, OrderDTO value) {
            try {
                var item = context.Session.GetString(key);
                OrderDTO order = JsonConvert.DeserializeObject<OrderDTO>(item);
                order = value;
                context.Session.SetString(key, JsonConvert.SerializeObject(order));
            }
            catch {
                context.Session.SetString(key, JsonConvert.SerializeObject(value));
            }
        }

        public OrderDTO GetOrderFromSession(HttpContext context, string key) {
            try {
                //var item = context.Session.GetString(key);
                //List<OrderDTO> orders = JsonConvert.DeserializeObject<List<OrderDTO>>(item);
                //OrderDTO order = new OrderDTO();
                //foreach (var o in orders) {
                //    if(o.Id == value) {
                //        order = o;
                //    }
                //}
                //return order;

                var item = context.Session.GetString(key);
                OrderDTO order = JsonConvert.DeserializeObject<OrderDTO>(item);

                return order;
            }
            catch {
                return null;
            }
        }
    }
}
