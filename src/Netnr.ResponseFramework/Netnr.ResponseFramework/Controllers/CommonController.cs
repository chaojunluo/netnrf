using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 公共、常用查询
    /// </summary>
    [Authorize]
    public class CommonController : Controller
    {
        [Description("公共查询：菜单树")]
        public string QueryMenu(string type)
        {
            string result = string.Empty;
            var listMenu = Func.Common.QuerySysMenuList(x => x.SmStatus == 1, false);
            if (type != "all")
            {
                #region 根据登录用户查询角色配置的菜单
                var userinfo = Func.Common.GetLoginUserInfo(HttpContext);
                if (!string.IsNullOrWhiteSpace(userinfo.RoleId))
                {
                    var role = Func.Common.QuerySysRoleEntity(x => x.SrId == userinfo.RoleId);
                    var menuArray = role.SrMenus.Split(',').ToList();

                    listMenu = listMenu.Where(x => menuArray.Contains(x.SmId)).ToList();
                }
                else
                {
                    listMenu = new List<Domain.SysMenu>();
                }
                #endregion
            }

            #region 把实体转为JSON节点实体
            var listNode = new List<TreeNodeVM>();
            foreach (var item in listMenu)
            {
                listNode.Add(new TreeNodeVM()
                {
                    id = item.SmId,
                    pid = item.SmPid,
                    text = item.SmName,
                    ext1 = item.SmUrl,
                    ext2 = item.SmIcon
                });
            }
            #endregion

            result = Core.TreeTo.ListToTree(listNode, "pid", "id", new List<string> { Guid.Empty.ToString() });

            if (string.IsNullOrWhiteSpace(result))
            {
                result = "[]";
            }

            return result;
        }

        [Description("公共查询：功能按钮树")]
        public string QueryButtonTree()
        {
            string result = string.Empty;
            var list = Func.Common.QuerySysButtonList(x => x.SbStatus == 1);

            #region 把实体转为JSON节点实体
            var listNode = new List<TreeNodeVM>();
            foreach (var item in list)
            {
                listNode.Add(new TreeNodeVM()
                {
                    id = item.SbId,
                    pid = item.SbPid,
                    text = item.SbBtnText,
                    ext1 = item.SbBtnIcon,
                    ext2 = item.SbBtnClass,
                    ext3 = item.SbDescribe
                });
            }
            #endregion

            result = Core.TreeTo.ListToTree(listNode, "pid", "id", new List<string> { Guid.Empty.ToString() });

            if (string.IsNullOrWhiteSpace(result))
            {
                result = "[]";
            }

            return result;
        }

        [Description("公共查询：角色列表")]
        public List<ValueTextVM> QueryRole()
        {
            using (var db = new ContextBase())
            {
                var query = from a in db.SysRole
                            where a.SrStatus == 1
                            orderby a.SrCreateTime
                            select new ValueTextVM
                            {
                                value = a.SrId,
                                text = a.SrName
                            };
                var list = query.ToList();
                return list;
            }
        }
    }
}