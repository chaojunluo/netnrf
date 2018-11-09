using System.ComponentModel;
using System.Web.Mvc;

namespace Netnr.ResponseFramework.Controllers
{
    public class HomeController : Controller
    {
        [Description("首页")]
        public ActionResult Index()
        {
            ViewData["user"] = Func.Common.GetLoginUserInfo();
            return View();
        }

        [Description("桌面")]
        public ActionResult Desk()
        {
            return View();
        }
    }
}
