using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netnr.Func.ViewModel
{
    /// <summary>
    /// 公共查询 接收、输出 对象
    /// </summary>
    public class QueryDataVM
    {
        /// <summary>
        /// 接收参数
        /// </summary>
        public class GetParams
        {
            /// <summary>
            /// 请求标识
            /// </summary>
            public string uri { get; set; }

            /// <summary>
            /// 表名
            /// </summary>
            public string tableName { get; set; } = "";

            /// <summary>
            /// 查询条件
            /// </summary>
            public string wheres { get; set; } = "";

            /// <summary>
            /// 是否启用分页 1分页
            /// </summary>
            public int pagination { get; set; } = 1;

            /// <summary>
            /// 页码 默认 1
            /// </summary>
            public int page { get; set; } = 1;

            /// <summary>
            /// 页量 默认 30
            /// </summary>
            public int rows { get; set; } = 30;

            /// <summary>
            /// 排序列名
            /// </summary>
            public string sort { get; set; }

            /// <summary>
            /// 排序方式 默认 asc
            /// </summary>
            public string order { get; set; } = "asc";

            /// <summary>
            /// 排序拼接
            /// </summary>
            public string sortOrderJoin { get; set; }

            /// <summary>
            /// 是否查询列信息 1不查询
            /// </summary>
            public int columnsExists { get; set; } = 0;

            /// <summary>
            /// 拓展参数 
            /// </summary>
            public string pe1 { get; set; }

            /// <summary>
            /// 拓展参数 
            /// </summary>
            public string pe2 { get; set; }

            /// <summary>
            /// 拓展参数 
            /// </summary>
            public string pe3 { get; set; }

        }

        /// <summary>
        /// 输出信息
        /// </summary>
        public class OutputResult
        {
            /// <summary>
            /// 总条数
            /// </summary>
            public int total { get; set; } = 0;

            /// <summary>
            /// 数据
            /// </summary>
            public object data { get; set; } = new List<object>();

            /// <summary>
            /// 列标题
            /// </summary>
            public object columns { get; set; } = new List<object>();

            /// <summary>
            /// 拓展信息 
            /// </summary>
            public string or1 { get; set; } = "";

            /// <summary>
            /// 拓展信息
            /// </summary>
            public string or2 { get; set; } = "";

            /// <summary>
            /// 拓展信息 
            /// </summary>
            public string or3 { get; set; } = "";
        }

        /// <summary>
        /// 根据查询条件实体转成sql语句
        /// </summary>
        /// <param name="jwhere"></param>
        /// <param name="SqlOrLinq">字符串引号类型，可选值：sql 或 linq ；sql是单引号，linq是双引号</param>
        /// <returns></returns>
        public static string SqlQueryWhere(JToken jwhere, string SqlOrLinq = "sql")
        {
            string result = string.Empty;

            var items = jwhere["items"];
            //多个
            if (items != null)
            {
                string andor = jwhere["andor"].ToString();
                var listw = new List<string>();

                int len = items.Count();
                for (int i = 0; i < len; i++)
                {
                    var wi = SqlQueryWhere(items[i], SqlOrLinq);
                    if (!string.IsNullOrWhiteSpace(wi))
                    {
                        listw.Add(wi);
                    }
                }
                result = "(" + string.Join(" " + andor + " ", listw) + ")";
            }
            else
            {
                result = SqlQueryWhereItem(jwhere, SqlOrLinq);
            }

            return result;
        }

        /// <summary>
        /// 一项条件转换
        /// </summary>
        /// <param name="Jitem"></param>
        /// <param name="SqlOrLinq">字符串引号类型，可选值：sql 或 linq ；sql是单引号，linq是双引号</param>
        /// <returns></returns>
        private static string SqlQueryWhereItem(JToken Jitem, string SqlOrLinq)
        {
            string result = string.Empty;

            //符号，sql单引号，linq双引号
            string qm = SqlOrLinq.ToLower() == "linq" ? "\"" : "'";

            //列字段，严格处理，防止注入
            string field = Jitem["field"].ToStringOrEmpty().Trim().Replace("'", "").Replace(";", "").Replace(" ", "").Replace("[", "").Replace("]", "");
            if (SqlOrLinq.ToLower() != "linq")
            {
                //sql语句，字段名加方括号，增强防止注入
                field = "[" + field + "]";
            }

            //关系符
            string relation = Jitem["relation"].ToStringOrEmpty();
            //值
            var value = Jitem["value"];

            if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(relation) || value == null)
            {
                return result;
            }

            #region 特殊值处理，非linq

            switch (value.ToStringOrEmpty())
            {
                case "IS NULL":
                    return field + " IS NULL";
                case "IS NULL 0":
                    return "ISNULL(" + field + ",'0') = '0'";
            }

            #endregion

            string rel = Common.DicSqlRelation[relation];
            switch (relation)
            {
                case "Equal":
                case "NotEqual":
                case "LessThan":
                case "GreaterThan":
                case "LessThanOrEqual":
                case "GreaterThanOrEqual":
                    {
                        string val = value.ToStringOrEmpty();
                        val = qm + val.OfSql() + qm;
                        result = field + rel + val;
                    }
                    break;
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    {
                        string val = value.ToStringOrEmpty().OfSql();
                        result = field + " LIKE " + qm + rel.Replace("LIKE", "").Trim().Replace("X", val) + qm;
                    }
                    break;
                case "BetweenAnd":
                    {
                        if (value.Count() == 2)
                        {
                            string v1 = value[0].ToString();
                            string v2 = value[1].ToString();
                            v1 = qm + v1.OfSql() + qm;
                            v2 = qm + v2.OfSql() + qm;
                            result = field + " BETWEEN " + v1 + " AND " + v2;
                        }
                    }
                    break;
                case "In":
                case "NotIn":
                    {
                        var list = new List<string>();
                        int len = value.Count();
                        for (int i = 0; i < len; i++)
                        {
                            list.Add(value[i].ToString());
                        }
                        string its = string.Empty;
                        foreach (var item in list)
                        {
                            its += "," + qm + item.OfSql() + qm;
                        }
                        its = its.TrimStart(',');
                        result = field + " " + rel + "(" + its + ")";
                    }
                    break;
            }

            return result;
        }

    }
}
