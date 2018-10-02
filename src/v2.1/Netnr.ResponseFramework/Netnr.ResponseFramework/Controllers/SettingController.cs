using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        #region 系统角色

        [Description("系统角色页面")]
        public IActionResult SysRole()
        {
            return View();
        }

        [Description("查询系统角色")]
        public string QuerySysRole(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();
            using (var db = new ContextBase())
            {
                var query = db.SysRole;
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("保存系统角色")]
        public string SaveSysRole(SysRole mo, string savetype)
        {
            int num = 0;
            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    mo.CreateTime = DateTime.Now;
                    db.SysRole.Add(mo);
                }
                else
                {
                    db.SysRole.Update(mo);
                }
                num = db.SaveChanges();
            }
            return num > 0 ? "success" : "fail";
        }

        [Description("删除系统角色")]
        public string DelSysRole(string id)
        {
            int num = 0;
            using (var db = new ContextBase())
            {
                if (db.SysUser.Where(x => x.RoleId == id).Count() > 0)
                {
                    return "exists";
                }
                else
                {
                    var mo = db.SysRole.Find(id);
                    db.SysRole.Remove(mo);
                    num = db.SaveChanges();
                }
            }
            return num > 0 ? "success" : "fail";
        }

        #endregion

        #region 系统用户

        [Description("系统用户页面")]
        public IActionResult SysUser()
        {
            return View();
        }

        [Description("查询系统用户")]
        public string QuerySysUser(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();
            using (var db = new ContextBase())
            {
                var query = from a in db.SysUser
                            join b in db.SysRole on a.RoleId equals b.Id
                            select new
                            {
                                a.Id,
                                a.Nickname,
                                a.RoleId,
                                a.Sign,
                                a.Status,
                                a.UserGroup,
                                a.UserName,
                                a.UserPwd,
                                a.CreateTime,
                                OldUserPwd = a.UserPwd,
                                RoleName = b.Name
                            };
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("保存系统用户")]
        public string SaveSysUser(SysUser mo, string savetype, string OldUserPwd)
        {
            int num = 0;
            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    if (db.SysUser.Where(x => x.UserName == mo.UserName).Count() > 0)
                    {
                        return "exists";
                    }
                    mo.CreateTime = DateTime.Now;
                    mo.UserPwd = Core.CalcTo.MD5(mo.UserPwd);
                    db.SysUser.Add(mo);
                }
                else
                {
                    if (db.SysUser.Where(x => x.UserName == mo.UserName && x.Id != mo.Id).Count() > 0)
                    {
                        return "exists";
                    }
                    if (mo.UserPwd != OldUserPwd)
                    {
                        mo.UserPwd = Core.CalcTo.MD5(mo.UserPwd);
                    }
                    db.SysUser.Update(mo);
                }
                num = db.SaveChanges();
            }
            return num > 0 ? "success" : "fail";
        }

        [Description("删除系统用户")]
        public string DelSysUser(string id)
        {
            int num = 0;
            using (var db = new ContextBase())
            {
                var mo = db.SysUser.Find(id);
                db.SysUser.Remove(mo);
                num = db.SaveChanges();
            }
            return num > 0 ? "success" : "fail";
        }

        #endregion

        #region 系统日志

        [Description("系统日志页面")]
        public IActionResult SysLog()
        {
            return View();
        }

        [Description("查询系统日志")]
        public string QuerySysLog(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();
            using (var db = new ContextBase())
            {
                var query = db.SysLog;
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }
        #endregion

        #region 表配置

        [Description("系统表配置页面")]
        public IActionResult SysTableConfig()
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
                //query = query.Where(x => EF.Functions.Like(x.TableName, "%log%"));
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("保存表配置")]
        public string SaveSysTableConfig(SysTableConfig mo, string savetype)
        {
            int num = 0;
            using (var db = new ContextBase())
            {
                if (savetype == "add")
                {
                    db.SysTableConfig.Add(mo);
                }
                else
                {
                    db.SysTableConfig.Update(mo);
                }
                num = db.SaveChanges();
            }
            return num > 0 ? "success" : "fail";
        }

        [Description("删除表配置")]
        public string DelSysTableConfig(string id)
        {
            int num = 0;
            using (var db = new ContextBase())
            {
                var mo = db.SysTableConfig.Find(id);
                db.SysTableConfig.Remove(mo);
                num = db.SaveChanges();
            }
            return num > 0 ? "success" : "fail";
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