using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 起始页
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        [Description("首页")]
        public IActionResult Index()
        {
            var vm = Func.Common.GetLoginUserInfo(HttpContext);
            return View(vm);
        }

        [Description("桌面")]
        public IActionResult Desk()
        {
            return View();
        }

        [Description("请升级你的浏览器")]
        [AllowAnonymous]
        public IActionResult UpdateBrowser()
        {
            return View();
        }

        [Description("向导")]
        public IActionResult Guide()
        {
            return View();
        }
    }
}
