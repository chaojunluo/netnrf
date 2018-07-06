using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RF.Controllers
{
    public class TableController : Controller
    {
        //
        // GET: /Table/

        public ActionResult DataGrid()
        {
            return View();
        }

        public ActionResult PropertyGrid()
        {
            return View();
        }

        public ActionResult TreeGrid()
        {
            return View();
        }

        public string QueryTree()
        {
            string sql = @"select *,'closed' as state from sys_menu where m_pid=0;
                           select * from sys_column where vname='sys_menu' and IFNULL(hide,'')!=1 order by l_order ASC";
            DataSet ds = DB.HelperSQLite.Query(sql);
            //输出数据
            CommonController.QueryOutputModel qom = new CommonController.QueryOutputModel();
            qom.data = ds.Tables[0];
            qom.columns = ds.Tables[1];
            return qom.ToJson();
        }

        /// <summary>
        /// TreeGrid请求子集
        /// </summary>
        /// <returns></returns>
        public string QueryTreeSub()
        {
            int id = Convert.ToInt32(RouteData.Values["id"]);
            string sql = "select * from sys_menu where m_pid=" + id;
            return DB.HelperSQLite.Query(sql).Tables[0].ToJson();
        }
    }
}
