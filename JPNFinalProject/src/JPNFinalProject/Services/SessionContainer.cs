using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JPNFinalProject.Controllers;
using JPNFinalProject.Data.DTO;

namespace JPNFinalProject.Services
{
    public class SessionContainer : Controller
    {
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

                foreach (var p in ProductController.productList) {
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
                //List<int> ids = new List<int>();
                var item = context.Session.GetString(key);
                List<int> productIds = JsonConvert.DeserializeObject<List<int>>(item);
                //foreach(var i in productIds) {
                //    if(ids.Contains(i) == false) {
                //        ids.Add(i);
                //    }
                //}
                //return ids.Count();

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
    }
}
