using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.Controllers
{
    public class HomeController : Controller
    {

        #region 框架首页

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 桌面

        public ActionResult Desk()
        {
            return View();
        }

        #endregion

    }
}
