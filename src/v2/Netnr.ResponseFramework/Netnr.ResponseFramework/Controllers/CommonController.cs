using System;
using System.Collections.Generic;
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
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
                    var role = Func.Common.QuerySysRoleEntity(x => x.ID == userinfo.RoleId);
                    var menuArray = role.Menus.Split(',').ToList();

                    listMenu = listMenu.Where(x => menuArray.Contains(x.ID)).ToList();
                }
                #endregion
            }

            #region 把实体转为JSON节点实体
            var listNode = new List<JSONodeVM>();
            foreach (var item in listMenu)
            {
                listNode.Add(new JSONodeVM()
                {
                    id = item.ID,
                    pid = item.PID,
                    text = item.Name,
                    ext1 = item.Url,
                    ext2 = item.Icon
                });
            }
            #endregion

            result = Func.Common.ListToTree(listNode, "pid", "id", new List<object> { Guid.Empty.ToString() });

            if (string.IsNullOrWhiteSpace(result))
            {
                result = "[]";
            }

            return result;
        }

        /// <summary>
        /// 公用查询 普通表数据
        /// </summary>
        /// <param name="param">接收查询参数</param>
        /// <returns></returns>
        [HttpPost]
        public string QueryData(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();

            //根据标识检索是否存在对应的数据表，（表字典DicTable 可过滤掉不让查询的表，也可以自定义DicTable表字典）
            //前端直传表名查询会有安全问题，所以通过uri标识在后台做对应且可过滤
            if (Func.Common.DicTable.ContainsKey(param.uri))
            {
                //表名
                param.tableName = Func.Common.DicTable[param.uri];

                //条件
                if (!string.IsNullOrWhiteSpace(param.wheres))
                {
                    var jwhere = JObject.Parse(param.wheres) as JToken;
                    param.wheres = QueryDataVM.SqlQueryWhere(jwhere);
                }
                if (string.IsNullOrWhiteSpace(param.wheres))
                {
                    param.wheres = "1=1";
                }

                //排序
                param.sortOrderJoin = DataBase.OrderByJoin(param.sort, param.order);

                //查询：列、数据表、总条数
                if (param.columnsExists != 1)
                {
                    or.columns = Func.Common.QuerySysTableConfigList(x => x.TableName == param.tableName);
                }
                var ds = Func.Common.QueryDS(param);
                or.data = ds.Tables[0];
                if (param.pagination == 1)
                {
                    or.total = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                }
                else
                {
                    or.total = ds.Tables[0].Rows.Count;
                }
            }

            return or.ToJson();
        }

        /// <summary>
        /// 查询功能按钮树
        /// </summary>
        /// <returns></returns>
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
                    id = item.ID,
                    pid = item.PID,
                    text = item.BtnText,
                    ext1 = item.BtnIcon,
                    ext2 = item.BtnClass,
                    ext3 = item.Describe
                });
            }
            #endregion

            result = Func.Common.ListToTree(listNode, "pid", "id", new List<object> { Guid.Empty.ToString() });

            if (string.IsNullOrWhiteSpace(result))
            {
                result = "[]";
            }

            return result;
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        public string QueryRole()
        {
            string result = "[]";
            using (var ru = new RepositoryUse())
            {
                var query = from a in ru.Context.Set<SysRole>()
                            where a.Status == 1
                            orderby a.CreateTime
                            select new
                            {
                                value = a.ID,
                                text = a.Name
                            };
                var list = query.ToList();
                result = list.ToJson();
            }
            return result;
        }

    }
}