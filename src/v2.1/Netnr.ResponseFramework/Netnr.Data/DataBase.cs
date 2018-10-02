using System;
using System.Linq;
using System.Linq.Expressions;

namespace Netnr.Data
{
    public class DataBase
    {
        /// <summary>
        /// 分页参数，不带排序
        /// </summary>
        public class Pagination
        {
            /// <summary>
            /// 页码
            /// </summary>
            public int PageNumber { get; set; }
            /// <summary>
            /// 页量
            /// </summary>
            public int PageSize { get; set; }
            /// <summary>
            /// 总数量
            /// </summary>
            public int Total { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int PageTotal
            {
                get
                {
                    int pt = 0;
                    try
                    {
                        pt = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Total) / Convert.ToDecimal(PageSize)));
                    }
                    catch (Exception)
                    {
                    }
                    return pt;
                }
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sorts">排序字段，支持多个，逗号分割</param>
        /// <param name="orders">排序类型，支持多个，逗号分割</param>
        public static IQueryable<T> OrderBy<T>(IQueryable<T> query, string sorts, string orders = "asc")
        {
            var listSort = sorts.Split(',').ToList();
            var listOrder = orders.Split(',').ToList();
            
            //倒叙
            for (int i = listSort.Count - 1; i >= 0; i--)
            {
                var sort = listSort[i];
                var order = i < listOrder.Count ? listOrder[i] : "asc";

                var property = typeof(T).GetProperties().Where(x => x.Name.ToLower() == sort.ToLower()).First();

                var parameter = Expression.Parameter(typeof(T), "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var lambda = Expression.Lambda(propertyAccess, parameter);

                if (order.ToLower() == "desc")
                {
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(lambda));
                    query = query.Provider.CreateQuery<T>(resultExp);
                }
                else
                {
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(lambda));
                    query = query.Provider.CreateQuery<T>(resultExp);
                }
            }

            return query;
        }

        /// <summary>
        /// 排序拼接
        /// </summary>
        /// <param name="sortName">排序字段，支持多个，逗号分隔</param>
        /// <param name="sortOrder">排序模式，支持多个，逗号分隔</param>
        /// <param name="bracket">排序字段带方括号 默认是</param>
        /// <returns></returns>
        public static string OrderBy(string sortName, string sortOrder, bool bracket = true)
        {
            string result = string.Empty;

            var sortlist = sortName.Split(',').ToList();
            var orderlist = sortOrder.Split(',').ToList();
            if (sortlist.Count == orderlist.Count)
            {
                for (int i = 0; i < sortlist.Count; i++)
                {
                    string sortField = sortlist[i].Trim().Replace("'", "").Replace(";", "").Replace(" ", "").Replace("[", "").Replace("]", "");
                    if (bracket)
                    {
                        sortField = "[" + sortField + "]";
                    }
                    string orderType = orderlist[i].ToLower().Trim() == "asc" ? "asc" : "desc";
                    result += sortField + " " + orderType + ",";
                }
            }

            return result.TrimEnd(',');
        }
    }
}

