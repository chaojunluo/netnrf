using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Description("首页")]
        public IActionResult Index()
        {
            return View();
        }

        [Description("桌面")]
        public IActionResult Desk()
        {
            return View();
        }
    }
}
