using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RF.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        #region 登录

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录授权
        /// </summary>
        /// <returns></returns>
        public string LoginAuth()
        {
            string username = Request.Form["username"].ToString();
            string password = Request.Form["password"].ToString();
            string captcha = Request.Form["captcha"].ToString();

            string result = DAL.AccountDAL.LoginAuth(username, password, captcha);

            return result;
        }


        #endregion

        #region 注销

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            DAL.PartialDAL.ClearCatchFoMenuButtonRole();
            return new RedirectResult("/account/login");
        }

        #endregion
    }
}
