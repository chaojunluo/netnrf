using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using Newtonsoft.Json.Linq;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [Authorize]
    public class SettingController : Controller
    {
        #region 系统按钮

        [Description("系统按钮页面")]
        public IActionResult SysButton()
        {
            return View();
        }

        [Description("查询系统按钮")]
        public QueryDataOutputVM QuerySysButton(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var list = db.SysButton.OrderBy(x => x.SbBtnOrder).ToList();
                var tree = Core.TreeTo.ListToTree(list, "SbPid", "SbId", new List<string> { Guid.Empty.ToString() });
                ovm.data = tree.ToJArray();

                //列
                if (ivm.columnsExists != 1)
                {
                    ovm.columns = db.SysTableConfig.Where(x => x.TableName == ivm.tableName).OrderBy(x => x.ColOrder).ToList();
                }
            }
            return ovm;
        }

        [Description("保存系统按钮")]
        public ActionResultVM SaveSysButton(SysButton mo, string savetype)
        {
            var vm = new ActionResultVM();

            if (string.IsNullOrWhiteSpace(mo.SbPid))
            {
                mo.SbPid = Guid.Empty.ToString();
            }
            if (mo.SbBtnHide == null)
            {
                mo.SbBtnHide = -1;
            }
            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    mo.SbId = Guid.NewGuid().ToString();
                    db.SysButton.Add(mo);
                }
                else
                {
                    db.SysButton.Update(mo);
                }

                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            //清理缓存
            Core.CacheTo.Remove(Func.Common.GlobalCacheKey.SysButton);

            return vm;
        }

        [Description("删除系统按钮")]
        public ActionResultVM DelSysButton(string id)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var mo = db.SysButton.Find(id);
                db.SysButton.Remove(mo);
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        #endregion

        #region 系统菜单

        [Description("系统菜单页面")]
        public IActionResult SysMenu()
        {
            return View();
        }

        [Description("查询系统菜单")]
        public QueryDataOutputVM QuerySysMenu(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var list = db.SysMenu.OrderBy(x => x.SmOrder).ToList();
                var tree = Core.TreeTo.ListToTree(list, "SmPid", "SmId", new List<string> { Guid.Empty.ToString() });
                ovm.data = tree.ToJArray();

                //列
                if (ivm.columnsExists != 1)
                {
                    ovm.columns = db.SysTableConfig.Where(x => x.TableName == ivm.tableName).OrderBy(x => x.ColOrder).ToList();
                }
            }
            return ovm;
        }

        [Description("保存系统菜单")]
        public ActionResultVM SaveSysMenu(SysMenu mo, string savetype)
        {
            var vm = new ActionResultVM();

            if (string.IsNullOrWhiteSpace(mo.SmPid))
            {
                mo.SmPid = Guid.Empty.ToString();
            }
            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    mo.SmId = Guid.NewGuid().ToString();
                    db.SysMenu.Add(mo);
                }
                else
                {
                    db.SysMenu.Update(mo);
                }
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            //清理缓存
            Core.CacheTo.Remove(Func.Common.GlobalCacheKey.SysMenu);

            return vm;
        }

        [Description("删除系统菜单")]
        public ActionResultVM DelSysMenu(string id)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var mo = db.SysMenu.Find(id);
                db.SysMenu.Remove(mo);

                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        #endregion

        #region 系统角色

        [Description("系统角色页面")]
        public IActionResult SysRole()
        {
            return View();
        }

        [Description("查询系统角色")]
        public QueryDataOutputVM QuerySysRole(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.SysRole;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        [Description("保存系统角色")]
        public ActionResultVM SaveSysRole(SysRole mo, string savetype)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    mo.SrId = Guid.Empty.ToString();
                    mo.SrCreateTime = DateTime.Now;
                    db.SysRole.Add(mo);
                }
                else
                {
                    db.SysRole.Update(mo);
                }
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        [Description("复制角色权限")]
        public ActionResultVM CopySysRoleAuth(SysRole mo, string copyid)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var list = db.SysRole.Where(x => x.SrId == mo.SrId || x.SrId == copyid).ToList();
                var copymo = list.Find(x => x.SrId == copyid);
                foreach (var item in list)
                {
                    item.SrMenus = copymo.SrMenus;
                    item.SrButtons = copymo.SrButtons;
                }
                db.SysRole.UpdateRange(list);
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        [Description("删除系统角色")]
        public ActionResultVM DelSysRole(string id)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                if (db.SysUser.Where(x => x.SrId == id).Count() > 0)
                {
                    vm.Set(ARTag.exist);
                }
                else
                {
                    var mo = db.SysRole.Find(id);
                    db.SysRole.Remove(mo);
                    int num = db.SaveChanges();

                    vm.Set(num > 0);
                }
            }

            return vm;
        }

        #endregion

        #region 系统用户

        [Description("系统用户页面")]
        public IActionResult SysUser()
        {
            return View();
        }

        [Description("查询系统用户")]
        public QueryDataOutputVM QuerySysUser(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = from a in db.SysUser
                            join b in db.SysRole on a.SrId equals b.SrId
                            select new
                            {
                                a.SuId,
                                a.SuNickname,
                                a.SrId,
                                a.SuSign,
                                a.SuStatus,
                                a.SuGroup,
                                a.SuName,
                                a.SuPwd,
                                a.SuCreateTime,
                                OldUserPwd = a.SuPwd,
                                b.SrName
                            };
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        [Description("保存系统用户")]
        public ActionResultVM SaveSysUser(SysUser mo, string savetype, string OldUserPwd)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    if (db.SysUser.Where(x => x.SuName == mo.SuName).Count() > 0)
                    {
                        vm.Set(ARTag.exist);
                    }
                    else
                    {
                        mo.SuId = Guid.NewGuid().ToString();
                        mo.SuCreateTime = DateTime.Now;
                        mo.SuPwd = Core.CalcTo.MD5(mo.SuPwd);
                        db.SysUser.Add(mo);
                    }
                }
                else
                {
                    if (db.SysUser.Where(x => x.SuName == mo.SuName && x.SuId != mo.SuId).Count() > 0)
                    {
                        vm.Set(ARTag.exist);
                    }
                    else
                    {
                        if (mo.SuPwd != OldUserPwd)
                        {
                            mo.SuPwd = Core.CalcTo.MD5(mo.SuPwd);
                        }
                        db.SysUser.Update(mo);
                    }
                }
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        [Description("删除系统用户")]
        public ActionResultVM DelSysUser(string id)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var mo = db.SysUser.Find(id);
                db.SysUser.Remove(mo);
                int num = db.SaveChanges();
                vm.Set(num > 0);
            }

            return vm;
        }

        #endregion

        #region 系统日志

        [Description("系统日志页面")]
        public IActionResult SysLog()
        {
            return View();
        }

        [Description("查询系统日志")]
        public QueryDataOutputVM QuerySysLog(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();
            using (var db = new ContextBase())
            {
                var query = db.SysLog;
                Func.Common.QueryJoin(query, ivm, db, ref ovm);
            }
            return ovm;
        }

        #endregion

        #region 表配置

        [Description("系统表配置页面")]
        public IActionResult SysTableConfig()
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

        [Description("保存表配置")]
        public ActionResultVM SaveSysTableConfig(SysTableConfig mo, List<string> ColRelation, string savetype)
        {
            var vm = new ActionResultVM();

            mo.ColRelation = string.Join(',', ColRelation);
            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    mo.Id = Guid.NewGuid().ToString();
                    db.SysTableConfig.Add(mo);
                }
                else
                {
                    db.SysTableConfig.Update(mo);
                }
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        [Description("删除表配置")]
        public ActionResultVM DelSysTableConfig(string id)
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                var mo = db.SysTableConfig.Find(id);
                db.SysTableConfig.Remove(mo);
                int num = db.SaveChanges();

                vm.Set(num > 0);
            }

            return vm;
        }

        #endregion

        #region 样式配置

        [Description("样式配置页面")]
        public IActionResult SysStyle()
        {
            return View();
        }

        #endregion
    }
}