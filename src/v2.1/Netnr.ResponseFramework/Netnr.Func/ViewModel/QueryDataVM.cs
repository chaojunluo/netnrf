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
    }
}
