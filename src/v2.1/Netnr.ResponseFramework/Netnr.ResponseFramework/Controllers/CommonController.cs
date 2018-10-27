using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using Newtonsoft.Json.Linq;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class CommonController : Controller
    {
        [Description("公共查询：菜单树")]
        [ResponseCache(Duration = 60)]
        public string QueryMenu(string type)
        {
            string result = string.Empty;
            var listMenu = Func.Common.QuerySysMenuList(x => x.Status == 1);
            if (type != "all")
            {
                #region 根据登录用户查询角色配置的菜单
                var userinfo = Func.Common.GetLoginUserInfo(HttpContext);
                if (!string.IsNullOrWhiteSpace(userinfo.RoleId))
                {
                    var role = Func.Common.QuerySysRoleEntity(x => x.Id == userinfo.RoleId);
                    var menuArray = role.Menus.Split(',').ToList();

                    listMenu = listMenu.Where(x => menuArray.Contains(x.Id)).ToList();
                }
                #endregion
            }

            #region 把实体转为JSON节点实体
            var listNode = new List<JSONodeVM>();
            foreach (var item in listMenu)
            {
                listNode.Add(new JSONodeVM()
                {
                    id = item.Id,
                    pid = item.Pid,
                    text = item.Name,
                    ext1 = item.Url,
                    ext2 = item.Icon
                });
            }
            #endregion

            result = Func.Common.ListToTree(listNode, "pid", "id", new List<string> { Guid.Empty.ToString() });

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
            var list = Func.Common.QuerySysButtonList(x => x.Status == 1);

            #region 把实体转为JSON节点实体
            var listNode = new List<JSONodeVM>();
            foreach (var item in list)
            {
                listNode.Add(new JSONodeVM()
                {
                    id = item.Id,
                    pid = item.Pid,
                    text = item.BtnText,
                    ext1 = item.BtnIcon,
                    ext2 = item.BtnClass,
                    ext3 = item.Describe
                });
            }
            #endregion

            result = Func.Common.ListToTree(listNode, "pid", "id", new List<string> { Guid.Empty.ToString() });

            if (string.IsNullOrWhiteSpace(result))
            {
                result = "[]";
            }

            return result;
        }

        [Description("公共查询：角色列表")]
        public string QueryRole()
        {
            string result = "[]";
            using (var db = new ContextBase())
            {
                var query = from a in db.SysRole
                            where a.Status == 1
                            orderby a.CreateTime
                            select new
                            {
                                value = a.Id,
                                text = a.Name
                            };
                var list = query.ToList();
                result = list.ToJson();
            }
            return result;
        }
    }
}