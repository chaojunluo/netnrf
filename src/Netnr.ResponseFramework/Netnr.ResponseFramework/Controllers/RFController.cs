using System.ComponentModel;
using System.Linq;
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

        [Description("查询表配置")]
        public string QuerySysTableConfig(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();
            using (var db = new ContextBase())
            {
                var query = db.SysTableConfig;
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("TreeGrid示例页面")]
        public IActionResult TreeGrid(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                using (var db = new ContextBase())
                {
                    var query = from a in db.SysMenu
                                where a.SmPid == id
                                orderby a.SmOrder
                                select new
                                {
                                    a.SmId,
                                    a.SmPid,
                                    a.SmName,
                                    a.SmUrl,
                                    a.SmOrder,
                                    a.SmIcon,
                                    a.SmStatus,
                                    a.SmGroup,
                                    //查询是否有子集
                                    state = (from b in db.SysMenu where b.SmPid == a.SmId select b.SmId).Count() > 0 ? "closed" : "open"
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
        
        [Description("表单示例页面")]
        public IActionResult Form()
        {
            return View();
        }
    }
}