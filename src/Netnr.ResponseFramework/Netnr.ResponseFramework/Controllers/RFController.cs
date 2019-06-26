using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Func.ViewModel;
using System;
using System.Collections.Generic;
using Netnr.Domain;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 示例，请删除
    /// </summary>
    public class RFController : Controller
    {
        #region 表配置示例

        [Description("表配置示例页面")]
        public IActionResult Tce()
        {
            return View();
        }

        [Description("查询表配置示例")]
        public QueryDataOutputVM QueryTempExample(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.TempExample;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        #endregion

        #region DataGrid示例页面

        [Description("DataGrid示例页面")]
        public IActionResult DataGrid()
        {
            return View();
        }

        [Description("查询表配置")]
        public QueryDataOutputVM QuerySysTableConfig(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.SysTableConfig;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        #endregion

        #region TreeGrid示例页面

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
        public QueryDataOutputVM QuerySysMenu(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.SysMenu;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        #endregion

        #region Grid多表格变动

        [Description("Grid多表格变动")]
        public IActionResult GridChange()
        {
            return View();
        }

        [Description("Grid多表格-主表")]
        public QueryDataOutputVM QueryGridChange1(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.SysRole;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        [Description("Grid多表格-子表")]
        public QueryDataOutputVM QueryGridChange2(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.SysUser;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        #endregion

        #region 生成多表单

        [Description("生成多表单")]
        public IActionResult BuildForms()
        {
            return View();
        }

        #endregion

        #region 静态表单示例页面

        [Description("静态表单示例页面")]
        public IActionResult Form()
        {
            return View();
        }

        #endregion

        #region 上传接口示例

        [Description("公共上传示例")]
        public IActionResult Upload()
        {
            return View();
        }

        #endregion

        #region 富文本

        [Description("嵌入富文本")]
        public IActionResult RichText()
        {
            return View();
        }

        #endregion

        #region Bulk Test，请手动修改 private 为 public 后测试

        [Description("批量新增")]
        private ActionResultVM BulkInsert()
        {
            var vm = new ActionResultVM();

            var list = new List<SysLog>();
            for (int i = 0; i < 50_000; i++)
            {
                var mo = new SysLog()
                {
                    LogId = Guid.NewGuid().ToString(),
                    LogAction = "/",
                    LogBrowserName = "Chrome",
                    LogCity = "重庆",
                    LogContent = "测试信息",
                    LogCreateTime = vm.startTime,
                    LogGroup = 1,
                    LogIp = "0.0.0.0",
                    LogSystemName = "Win10",
                    LogUrl = Request.Path,
                    SuName = "netnr",
                    SuNickname = "netnr",
                    LogRemark = "无"
                };
                list.Add(mo);
            }

            using (var db = new ContextBase())
            {
                db.SysLog.BulkInsert(list);

                db.BulkSaveChanges();

                vm.Set(ARTag.success);
            }

            return vm;
        }

        [Description("批量修改")]
        private ActionResultVM BulkUpdate()
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var list = db.SysLog.OrderBy(x => x.LogCreateTime).Take(50_000).ToList();

                foreach (var item in list)
                {
                    item.LogRemark = Guid.NewGuid().ToString();
                }

                db.SysLog.BulkUpdate(list);

                db.BulkSaveChanges();

                vm.Set(ARTag.success);
            }

            return vm;
        }

        [Description("批量删除")]
        private ActionResultVM BulkDelete()
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var list = db.SysLog.OrderBy(x => x.LogCreateTime).Take(50_000).ToList();

                db.SysLog.BulkDelete(list);

                db.BulkSaveChanges();

                vm.Set(ARTag.success);
            }

            return vm;
        }

        #endregion
    }
}