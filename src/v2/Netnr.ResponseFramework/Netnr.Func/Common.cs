using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Netnr.Func
{
    public class Common
    {
        #region 字典  

        private static Dictionary<string, string> _DicTable;
        /// <summary>
        /// 数据库表字典（根据生成的DLL读取信息）
        /// </summary>
        public static Dictionary<string, string> DicTable
        {
            get
            {
                if (_DicTable == null)
                {
                    var ass = Assembly.Load("Netnr.Domain.dll").GetTypes();
                    var dic = new Dictionary<string, string>();
                    foreach (var item in ass)
                    {
                        dic.Add(item.Name.ToLower(), item.Name);
                    }
                    _DicTable = dic;
                }
                return _DicTable;
            }
            set => _DicTable = value;
        }

        private static Dictionary<string, string> _dicSqlRelation;
        /// <summary>
        /// 数据库查询条件关系符
        /// </summary>
        public static Dictionary<string, string> DicSqlRelation
        {
            get
            {
                if (_dicSqlRelation == null)
                {
                    var ts = @"
                                Equal: '=',
                                NotEqual: '!=',
                                LessThan: '<',
                                GreaterThan: '>',
                                LessThanOrEqual: '<=',
                                GreaterThanOrEqual: '>=',
                                BetweenAnd: 'BETWEEN A AND B',
                                Contains: 'LIKE %X%',
                                StartsWith: 'LIKE X%',
                                EndsWith: 'LIKE %X',
                                In: 'IN',
                                NotIn: 'NOT IN'
                              ".Split(',').ToList();
                    var dic = new Dictionary<string, string>();
                    foreach (var item in ts)
                    {
                        var ms = item.Split(':').ToList();
                        dic.Add(ms.FirstOrDefault().Trim(), ms.LastOrDefault().Trim().Replace("'", ""));
                    }
                    _dicSqlRelation = dic;
                }
                return _dicSqlRelation;
            }
            set
            {
                _dicSqlRelation = value;
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 数据集合转JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pidField">父ID键</param>
        /// <param name="idField">ID键</param>
        /// <param name="listStartId">开始的PID</param>
        /// <returns></returns>
        public static string ListToTree<T>(List<T> list, string pidField, string idField, List<object> listStartId)
        {
            StringBuilder sbTree = new StringBuilder();

            var rdt = list.Where(Core.LambdaTo.Contains<T>(pidField, listStartId).Compile()).ToList();

            for (int i = 0; i < rdt.Count; i++)
            {
                //数组“[”开始
                if (i == 0)
                {
                    sbTree.Append("[");
                }
                else
                {
                    sbTree.Append(",");
                }

                sbTree.Append("{");

                //数据行
                var dr = rdt[i];
                string mojson = dr.ToJson();
                sbTree.Append(mojson.TrimStart('{').TrimEnd('}'));

                var pis = dr.GetType().GetProperties();

                var pi = pis.Where(x => x.Name == idField).FirstOrDefault();
                listStartId.Clear();
                object id = pi.GetValue(dr, null);
                listStartId.Add(id);

                var nrdt = list.Where(Core.LambdaTo.Equal<T>(pidField, id).Compile()).ToList();

                if (nrdt.Count > 0)
                {
                    string rs = ListToTree(list, pidField, idField, listStartId);

                    //子数组源于递归
                    sbTree.Append(",\"children\":" + rs + "}");
                }
                else
                {
                    sbTree.Append("}");
                }

                //数组结束“]”
                if (i == rdt.Count - 1)
                {
                    sbTree.Append("]");
                }
            }

            return sbTree.ToString();
        }

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <returns></returns>
        //public static DataSet QueryDS(QueryDataVM.GetParams param)
        //{
        //    var ds = new DataSet();

        //    string sql = string.Empty;
        //    //分页
        //    if (param.pagination == 1)
        //    {
        //        sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + param.sortOrderJoin + ")AS NumId, " + param.columnsFields + "  FROM " + param.tableName + " WHERE " + param.wheres + ") T WHERE NumId BETWEEN " + ((param.page - 1) * param.rows + 1) + " AND " + param.page * param.rows;
        //        sql += "; SELECT COUNT(1) AS total FROM " + param.tableName + " WHERE " + param.wheres;
        //    }
        //    else
        //    {
        //        sql = "SELECT " + param.columnsFields + " FROM " + param.tableName + " WHERE " + param.wheres + " ORDER BY " + param.sortOrderJoin;
        //    }
        //    ds = SQLServerDB.GetDataSet(sql);

        //    return ds;
        //}

        /// <summary>
        /// 查询拼接，linq追加sql条件，不支持like、between and等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="ru"></param>
        /// <param name="or"></param>
        public static void QueryJoin<T>(IQueryable<T> query, QueryDataVM.GetParams param, RepositoryUse ru, ref QueryDataVM.OutputResult or)
        {
            //条件
            if (!string.IsNullOrWhiteSpace(param.wheres))
            {
                var jwhere = JObject.Parse(param.wheres) as JToken;
                param.wheres = QueryDataVM.SqlQueryWhere(jwhere, "linq");
            }

            if (!string.IsNullOrWhiteSpace(param.wheres))
            {
                query = DynamicQueryableExtensions.Where(query, param.wheres);
            }
            //总条数
            or.total = query.Count();
            //分页
            if (param.pagination == 1)
            {
                query = query.Skip((param.page - 1) * param.rows).Take(param.rows);
            }
            //排序
            query = DataBase.OrderBy(query, param.sort, param.order);
            //数据
            or.data = query.ToList();

            //列
            if (param.columnsExists != 1)
            {
                or.columns = ru.SysTableConfigRepository.IQueryable(x => x.TableName == param.tableName).OrderBy(x => x.ColOrder).ToList();
            }
        }

        public static string IQuery(QueryDataVM.GetParams param)
        {
            var rt = new QueryDataVM.OutputResult();

            using (var ru = new RepositoryUse())
            {
                IQueryable<object> query = null;

                switch (param.tableName.ToLower())
                {
                    case "sysuser":
                        query = DataBase.OrderBy(from a in ru.Context.Set<SysUser>() select a, param.sort, param.order);
                        break;
                }

                if (query != null)
                {
                    //总数
                    rt.total = query.Count();
                    //分页
                    if (param.pagination == 1)
                    {
                        query = query.Skip((param.page - 1) * param.rows).Take(param.rows);
                    }
                    //数据
                    rt.data = query.ToList();

                    //查询列配置
                    if (param.columnsExists != 1)
                    {
                        rt.columns = ru.SysTableConfigRepository.IQueryable(x => x.TableName == param.tableName).ToList();
                    }
                }
            }

            return rt.ToJson();
        }

        /// <summary>
        /// 查询拼接，自定义查询SQL主体
        /// 关联查询时，注意避免相同字段列，如过查询条件字段存在多张表中则会出现字段不明确的错误
        /// </summary>
        /// <param name="sql">查询SQL主体，注意：避免相同字段列</param>
        /// <param name="param"></param>
        /// <param name="or"></param>
        /// <param name="sqlcount">查询SQL总数量，默认查询SQL主体，注意：避免相同字段列</param>
        //public static void QueryJoin(string sql, QueryDataVM.GetParams param, ref QueryDataVM.OutputResult or, string sqlcount = null)
        //{
        //    //条件
        //    if (!string.IsNullOrWhiteSpace(param.wheres))
        //    {
        //        var jwhere = JObject.Parse(param.wheres) as JToken;
        //        param.wheres = QueryDataVM.SqlQueryWhere(jwhere);
        //    }
        //    if (string.IsNullOrWhiteSpace(param.wheres))
        //    {
        //        param.wheres = " WHERE 1=1";
        //    }
        //    else
        //    {
        //        param.wheres = " WHERE " + param.wheres;
        //    }

        //    //排序
        //    param.sortOrderJoin = DataBase.OrderByJoin(param.sort, param.order);

        //    //总条数
        //    if (sqlcount == null)
        //    {
        //        sqlcount = "SELECT COUNT(1) FROM (" + sql + ") CR " + param.wheres;
        //    }
        //    else
        //    {
        //        sqlcount = sqlcount + param.wheres;
        //    }

        //    //排序分页
        //    if (param.pagination == 1)
        //    {
        //        sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + param.sortOrderJoin + ") AS NumId,* FROM("
        //            + sql + ")T " + param.wheres + " ) TT WHERE NumId BETWEEN "
        //            + ((param.page - 1) * param.rows + 1) + " AND " + param.page * param.rows;
        //    }
        //    else
        //    {
        //        //排序不分页
        //        sql += "SELECT * FROM (" + sql + ") T " + param.wheres + " ORDER BY " + param.sortOrderJoin;
        //    }

        //    //查询数据
        //    var ds = SQLServerDB.GetDataSet(sql + ";" + Environment.NewLine + sqlcount);

        //    //数据
        //    or.data = ds.Tables[0];
        //    //总条数
        //    or.total = Convert.ToInt32(ds.Tables[ds.Tables.Count - 1].Rows[0][0].ToString());

        //    //列
        //    if (param.columnsExists != 1)
        //    {
        //        using (var ru = new RepositoryUse())
        //        {
        //            or.columns = ru.SysTableConfigRepository.IQueryable(x => x.TableName == param.tableName).OrderBy(x => x.ColOrder).ToList();
        //        }
        //    }
        //}

        #endregion

        #region 获取登录用户信息

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public static LoginUserVM GetLoginUserInfo(HttpContext context)
        {
            var loginUser = new LoginUserVM
            {
                UserId = context.User.FindFirst(ClaimTypes.Sid)?.Value,
                UserName = context.User.FindFirst(ClaimTypes.Name)?.Value,
                Nickname = context.User.FindFirst(ClaimTypes.GivenName)?.Value,
                RoleId = context.User.FindFirst(ClaimTypes.Role)?.Value
            };

            return loginUser;
        }

        /// <summary>
        /// 获取登录用户角色信息
        /// </summary>
        /// <param name="context"></param>
        public static SysRole LoginUserRoleInfo(HttpContext context)
        {
            var lui = GetLoginUserInfo(context);
            if (!string.IsNullOrWhiteSpace(lui.RoleId))
            {
                return QuerySysRoleEntity(x => x.ID == lui.RoleId);
            }
            return null;
        }

        #endregion

        #region 全局缓存KEY

        public class GlobalCacheKey
        {
            /// <summary>
            /// 菜单缓存KEY
            /// </summary>
            public const string SysMenu = "GlobalSysMenu";

            /// <summary>
            /// 按钮缓存KEY
            /// </summary>
            public const string SysButton = "GlobalSysButton";
        }

        /// <summary>
        /// 清空全局缓存
        /// </summary>
        public static void GlobalCacheRmove()
        {
            Core.CacheTo.Remove(GlobalCacheKey.SysMenu);
            Core.CacheTo.Remove(GlobalCacheKey.SysButton);
        }

        #endregion

        #region 查询系统表

        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<SysTableConfig> QuerySysTableConfigList(Expression<Func<SysTableConfig, bool>> predicate)
        {
            using (var ru = new RepositoryUse())
            {
                var list = ru.SysTableConfigRepository.IQueryable(predicate).OrderBy(x => x.ColOrder).ToList();
                return list;
            }
        }

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        public static List<SysMenu> QuerySysMenuList(Func<SysMenu, bool> predicate, bool cache = true)
        {
            var list = Core.CacheTo.Get(GlobalCacheKey.SysMenu) as List<SysMenu>;
            if (!cache || list == null)
            {
                using (var ru = new RepositoryUse())
                {
                    list = ru.SysMenuRepository.IQueryable().OrderBy(x => x.MenuOrder).ToList();
                    Core.CacheTo.Set(GlobalCacheKey.SysMenu, list, 300, false);
                }
            }
            list = list.Where(predicate).ToList();
            return list;
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<SysButton> QuerySysButtonList(Func<SysButton, bool> predicate, bool cache = true)
        {
            var list = Core.CacheTo.Get(GlobalCacheKey.SysButton) as List<SysButton>;
            if (!cache || list == null)
            {
                using (var ru = new RepositoryUse())
                {
                    list = ru.SysButtonRepository.IQueryable().OrderBy(x => x.BtnOrder).ToList();
                    Core.CacheTo.Set(GlobalCacheKey.SysButton, list, 300, false);
                }
            }
            list = list.Where(predicate).ToList();
            return list;
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static SysUser QuerySysUserEntity(Expression<Func<SysUser, bool>> predicate)
        {
            using (var ru = new RepositoryUse())
            {
                return ru.SysUserRepository.IQueryable(predicate).FirstOrDefault();
            }
        }

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static SysRole QuerySysRoleEntity(Expression<Func<SysRole, bool>> predicate)
        {
            using (var ru = new RepositoryUse())
            {
                var mo = ru.SysRoleRepository.IQueryable(predicate).FirstOrDefault();
                return mo;
            }
        }

        #endregion

    }
}
