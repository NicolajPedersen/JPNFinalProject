﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPNFinalProject.Services
{
    public class SessionContainer : Controller
    {
        public void AddToSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public string GetSessionString(string key)
        {
            return HttpContext.Session.GetString(key);
        }
    }
}