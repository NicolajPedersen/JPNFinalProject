using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JPNFinalProject.Services
{
    public class SessionContainer : Controller
    {
        public void AddToSession(HttpContext context, string key, int value) {
            try {
                var item = HttpContext.Session.GetString(key);
                List<int> basket1 = JsonConvert.DeserializeObject<List<int>>(item);
                basket1.Add(value);
                context.Session.SetString(key, JsonConvert.SerializeObject(basket1));
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

        public List<int> GetBasket(HttpContext context, string key) {
            try {
                var item = context.Session.GetString(key);
                List<int> productIds = JsonConvert.DeserializeObject<List<int>>(item);
                return productIds;
            }
            catch {
                return null;
            }
        }
    }
}
