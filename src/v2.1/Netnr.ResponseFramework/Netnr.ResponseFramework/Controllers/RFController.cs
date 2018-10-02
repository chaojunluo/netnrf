using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    public class RFController : Controller
    {
        [Description("表配置示例页面")]
        public IActionResult Tce()
        {
            return View();
        }

        [Description("查询表配置示例")]
        public string QueryTempExample(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();
            using (var db = new ContextBase())
            {
                var query = db.TempExample;
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("DataGrid示例页面")]
        public IActionResult DataGrid()
        {
            return View();
        }

        [Description("TreeGrid示例页面")]
        public IActionResult TreeGrid(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                using (var db = new ContextBase())
                {
                    var query = from a in db.SysMenu
                                where a.Pid == id
                                orderby a.MenuOrder
                                select new
                                {
                                    a.Id,
                                    a.Pid,
                                    a.Name,
                                    a.Url,
                                    a.MenuOrder,
                                    a.Icon,
                                    a.Status,
                                    a.MenuGroup,
                                    //查询是否有子集
                                    state = (from b in db.SysMenu where b.Pid == a.Id select b.Id).Count() > 0 ? "closed" : "open"
                                };
                    var list = query.ToList();
                    return Content(list.ToJson());
                }
            }

            return View();
        }

        [Description("查询系统菜单")]
        public string QuerySysMenu(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();
            using (var db = new ContextBase())
            {
                var query = db.SysMenu;
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("PropertyGrid示例页面")]
        public IActionResult PropertyGrid()
        {
            return View();
        }

        [Description("模态框表单示例页面")]
        public IActionResult Modal()
        {
            return View();
        }

        [Description("表单示例页面")]
        public IActionResult Form()
        {
            return View();
        }

    }
}