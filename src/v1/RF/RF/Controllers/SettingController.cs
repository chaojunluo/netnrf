using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    public class SettingController : Controller
    {
        #region 系统角色

        public ActionResult SysRole()
        {
            //菜单导航
            ViewBag.MenuTreeJson = new CommonController().QueryMenuTree();

            return View();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public string SaveSysRole(Model.sys_role mo)
        {
            string savetype = Request.QueryString["savetype"].ToString();
            mo.r_state = string.IsNullOrEmpty(Request.Form["r_state"]) ? 0 : 1;

            bool b = false;
            if (savetype == "0")
            {
                if (new DAL.sys_role().Add(mo) >= 0)
                    b = true;
            }
            else
            {
                mo.id = Convert.ToInt32(savetype);
                b = new DAL.sys_role().Update(mo);
            }
            return b ? "success" : "fail";
        }

        /// <summary>
        /// 保存角色权限配置
        /// </summary>
        /// <returns></returns>
        public string SaveSysRoleAuth()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string menus = Request.Form["r_menus"];
            string buttons = Request.Form["r_buttons"];
            bool b = DAL.SettingDAL.SaveSysRoleAuth(id, menus, buttons);
            return b ? "success" : "fail";
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public string DelSysRole()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            bool b = new DAL.sys_role().Delete(id);
            return b ? "success" : "fail";
        }

        #endregion

        #region 系统用户

        public ActionResult SysUser()
        {
            return View();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public string SaveSysUser(Model.sys_user mo)
        {
            string savetype = Request.QueryString["savetype"].ToString();
            mo.u_state = string.IsNullOrEmpty(Request.Form["u_state"]) ? 0 : 1;

            bool b = false;
            if (savetype == "0")
            {
                mo.id = Guid.NewGuid().ToString();
                mo.u_date = DateTime.Now;
                mo.u_pwd = DB.CalcTo.MD5(mo.u_pwd);
                b = new DAL.sys_user().Add(mo);
            }
            else
            {
                mo.id = savetype;
                if (mo.u_pwd.Length != 32)
                {
                    mo.u_pwd = DB.CalcTo.MD5(mo.u_pwd);
                }
                b = new DAL.sys_user().Update(mo);
            }
            return b ? "success" : "fail";
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public string DelSysUser()
        {
            string id = Request.QueryString["id"].ToString();
            bool b = new DAL.sys_user().Delete(id);
            return b ? "success" : "fail";
        }

        #endregion

        #region 系统日志

        public ActionResult SysLog()
        {
            return View();
        }

        #endregion

        #region 列表配置

        public ActionResult SysColumn()
        {
            return View();
        }

        /// <summary>
        /// 保存 列配置
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public string SaveSysColumn(Model.sys_column mo)
        {
            bool b = false;
            string savetype = Request.QueryString["savetype"].ToString();

            //新增
            if (savetype == "0")
            {
                if (new DAL.sys_column().Add(mo) > 0)
                    b = true;
            }
            else
            {
                b = new DAL.sys_column().Update(mo);
            }

            return b ? "success" : "fail";
        }


        /// <summary>
        /// 删除 列配置
        /// </summary>
        /// <returns></returns>
        public string DelSysColumn()
        {
            int id = Convert.ToInt32(RouteData.Values["Id"]);
            bool b = new DAL.sys_column().Delete(id);
            return b ? "success" : "fail";
        }

        #endregion

        #region 字体配置

        public ActionResult SysStyleConfig()
        {
            return View();
        }

        #endregion
    }
}
