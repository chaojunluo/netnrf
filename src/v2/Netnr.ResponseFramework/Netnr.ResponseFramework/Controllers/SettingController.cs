using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        #region 系统角色

        public IActionResult SysRole()
        {
            return View();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public string SaveSysRole(SysRole mo, string savetype)
        {
            int num = 0;
            using (var ru = new RepositoryUse())
            {
                if (savetype == "add")
                {
                    mo.CreateTime = DateTime.Now;
                    num = ru.SysRoleRepository.Insert(mo);
                }
                else
                {
                    num = ru.SysRoleRepository.Update(mo);
                }

            }
            return num > 0 ? "success" : "fail";
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public string DelSysRole(string id)
        {
            int num = 0;
            using (var ru = new RepositoryUse())
            {
                if (ru.SysUserRepository.IQueryable(x => x.RoleID == id).Count() > 0)
                {
                    return "exists";
                }
                else
                {
                    num = ru.SysRoleRepository.Delete(x => x.ID == id);
                }
            }
            return num > 0 ? "success" : "fail";
        }

        #endregion

        #region 系统用户

        public IActionResult SysUser()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string QuerySysUser(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();

            #region 自定义SQL查询主体

            //查询主体，select 开头 ，QueryJoin里面会处理分页排序及查询条件
            string sql = "select a.*,UserPwd as OldUserPwd,b.Name as RoleName from SysUser a left join SysRole b on a.RoleID=b.ID";
            //查询总条数，可选，不传，则以查询主体sql查询总条数，此处如果用主体SQL多了不必要的关联查询
            string sqlcount = "select count(1) from SysUser";

            Func.Common.QueryJoin(sql, param, ref or, sqlcount);

            #endregion

            #region linq查询，Like等SQL查询条件不支持
            //using (var ru = new RepositoryUse())
            //{
            //    var query = from a in ru.Context.Set<SysUser>()
            //                join b in ru.Context.Set<SysRole>() on a.RoleID equals b.ID
            //                select new
            //                {
            //                    a.ID,
            //                    a.RoleID,
            //                    a.UserName,
            //                    a.UserPwd,
            //                    OldUserPwd = a.UserPwd,
            //                    a.Nickname,
            //                    a.CreateTime,
            //                    a.Status,
            //                    a.UserGroup,
            //                    RoleName = b.Name
            //                };
            //    Func.Common.QueryJoin(query, param, ru, ref or);
            //}
            #endregion

            return or.ToJson();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public string SaveSysUser(SysUser mo, string savetype, string OldUserPwd)
        {
            int num = 0;
            using (var ru = new RepositoryUse())
            {
                if (savetype == "add")
                {
                    if (ru.SysUserRepository.IQueryable(x => x.UserName == mo.UserName).Count() > 0)
                    {
                        return "exists";
                    }
                    mo.CreateTime = DateTime.Now;
                    mo.UserPwd = Core.CalcTo.MD5(mo.UserPwd);
                    num = ru.SysUserRepository.Insert(mo);
                }
                else
                {
                    if (ru.SysUserRepository.IQueryable(x => x.UserName == mo.UserName && x.ID != mo.ID).Count() > 0)
                    {
                        return "exists";
                    }
                    if (mo.UserPwd != OldUserPwd)
                    {
                        mo.UserPwd = Core.CalcTo.MD5(mo.UserPwd);
                    }
                    num = ru.SysUserRepository.Update(mo);
                }
            }
            return num > 0 ? "success" : "fail";
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public string DelSysUser(string id)
        {
            int num = 0;
            using (var ru = new RepositoryUse())
            {
                num = ru.SysUserRepository.Delete(x => x.ID == id);
            }
            return num > 0 ? "success" : "fail";
        }

        #endregion

        #region 系统日志

        public IActionResult SysLog()
        {
            return View();
        }

        #endregion

        #region 表配置

        public IActionResult SysTableConfig()
        {
            return View();
        }

        /// <summary>
        /// 保存 表配置
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public string SaveSysTableConfig(SysTableConfig mo, string savetype)
        {
            int num = 0;
            using (var ru = new RepositoryUse())
            {
                if (savetype == "add")
                {
                    num = ru.SysTableConfigRepository.Insert(mo);
                }
                else
                {
                    num = ru.SysTableConfigRepository.Update(mo);
                }
            }
            return num > 0 ? "success" : "fail";
        }

        /// <summary>
        /// 删除 表配置
        /// </summary>
        /// <returns></returns>
        public string DelSysTableConfig(string id)
        {
            int num = 0;
            using (var ru = new RepositoryUse())
            {
                num = ru.SysTableConfigRepository.Delete(x => x.ID == id);
            }
            return num > 0 ? "success" : "fail";
        }

        #endregion

        #region 样式配置

        public IActionResult SysStyle()
        {
            return View();
        }

        #endregion
    }
}