using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Netnr.Func.ViewModel;
using Netnr.Domain;
using Netnr.Data;
using System;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("");
        }
    }
}