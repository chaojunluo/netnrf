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
                                Equal: '{0} = {1}',
                                NotEqual: '{0} != {1}',
                                LessThan: '{0} < {1}',
                                GreaterThan: '{0} > {1}',
                                LessThanOrEqual: '{0} <= {1}',
                                GreaterThanOrEqual: '{0} >= {1}',
                                BetweenAnd: '{0} >= {1} AND {0} <= {2}',
                                Contains: '%{0}%',
                                StartsWith: '{0}%',
                                EndsWith: '%{0}',
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
        /// 查询拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="ru"></param>
        /// <param name="or"></param>
        public static void QueryJoin<T>(IQueryable<T> query, QueryDataVM.GetParams param, ContextBase db, ref QueryDataVM.OutputResult or)
        {
            //条件
            query = QueryWhere(query, param);

            //总条数
            or.total = query.Count();

            //排序
            query = DataBase.OrderBy(query, param.sort, param.order);

            //分页
            if (param.pagination == 1)
            {
                query = query.Skip((param.page - 1) * param.rows).Take(param.rows);
            }

            //数据
            or.data = query.ToList();

            //列
            if (param.columnsExists != 1)
            {
                or.columns = db.SysTableConfig.Where(x => x.TableName == param.tableName).OrderBy(x => x.ColOrder).ToList();
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IQueryable<T> QueryWhere<T>(IQueryable<T> query, QueryDataVM.GetParams param)
        {
            //条件
            if (!string.IsNullOrWhiteSpace(param.wheres))
            {
                var whereItems = JArray.Parse(param.wheres);
                foreach (var item in whereItems)
                {
                    //关系符
                    var relation = item["relation"].ToStringOrEmpty();
                    string rel = DicSqlRelation[relation];

                    //字段
                    var field = item["field"].ToStringOrEmpty();
                    //值
                    var value = item["value"];

                    //值引号
                    var vqm = "\"";

                    switch (relation)
                    {
                        case "Equal":
                        case "NotEqual":
                        case "LessThan":
                        case "GreaterThan":
                        case "LessThanOrEqual":
                        case "GreaterThanOrEqual":
                            {
                                string val = vqm + value.ToStringOrEmpty() + vqm;
                                string iwhere = string.Format(rel, field, val);
                                query = DynamicQueryableExtensions.Where(query, iwhere);
                            }
                            break;
                        case "Contains":
                        case "StartsWith":
                        case "EndsWith":
                            {
                                string iwhere = string.Format(rel, value.ToStringOrEmpty());
                                query = query.Where(x => EF.Functions.Like(x.GetType().GetProperty(field).GetValue(x, null).ToString(), iwhere));
                            }
                            break;
                        case "BetweenAnd":
                            if (value.Count() == 2)
                            {
                                var v1 = vqm + value[0].ToString() + vqm;
                                var v2 = vqm + value[1].ToString() + vqm;

                                var iwhere = string.Format(rel, field, v1, v2);
                                query = DynamicQueryableExtensions.Where(query, iwhere);
                            }
                            break;
                        case "In":
                            {
                                var list = new List<string>();
                                int len = value.Count();
                                for (int i = 0; i < len; i++)
                                {
                                    list.Add(value[i].ToString());
                                }
                                query = query.Where(x => list.Contains(x.GetType().GetProperty(field).GetValue(x, null).ToString()));
                            }
                            break;
                        case "NotIn":
                            {
                                var list = new List<string>();
                                int len = value.Count();
                                for (int i = 0; i < len; i++)
                                {
                                    list.Add(value[i].ToString());
                                }
                                query = query.Where(x => !list.Contains(x.GetType().GetProperty(field).GetValue(x, null).ToString()));
                            }
                            break;
                    }
                }
            }
            return query;
        }

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
                return QuerySysRoleEntity(x => x.Id == lui.RoleId);
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
            using (var db = new ContextBase())
            {
                var list = db.SysTableConfig.Where(predicate).OrderBy(x => x.ColOrder).ToList();
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
                using (var db = new ContextBase())
                {
                    list = db.SysMenu.OrderBy(x => x.MenuOrder).ToList();
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
                using (var db = new ContextBase())
                {
                    list = db.SysButton.OrderBy(x => x.BtnOrder).ToList();
                    Core.CacheTo.Set(GlobalCacheKey.SysButton, list, 300, false);
                }
            }
            list = list.Where(predicate).ToList();
            return list;
        }

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static SysRole QuerySysRoleEntity(Expression<Func<SysRole, bool>> predicate)
        {
            using (var db = new ContextBase())
            {
                var mo = db.SysRole.Where(predicate).FirstOrDefault();
                return mo;
            }
        }

        #endregion

    }
}
