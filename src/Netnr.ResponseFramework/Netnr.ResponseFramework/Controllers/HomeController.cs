using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Description("首页")]
        public IActionResult Index()
        {
            ViewData["user"] = Func.Common.GetLoginUserInfo(HttpContext);
            return View();
        }

        [Description("桌面")]
        public IActionResult Desk()
        {
            return View();
        }
    }
}
