using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace RF.Controllers
{
    /// <summary>
    /// 公用
    /// </summary>
    public class CommonController : Controller
    {

        #region 验证码
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public FileResult Captcha()
        {
            //生成验证码
            string num = DB.Common.RandomCode();
            Session["captcha"] = num;
            byte[] bytes = DB.Common.CreateImg(num);
            return File(bytes, @"image/jpeg");
        }

        #endregion

        #region 生成 Tree-Json

        /// <summary>
        /// 生成TreeJson
        /// </summary>
        /// <returns></returns>
        public string TreeJson(List<TreeNode> list, string pid, int deep)
        {
            StringBuilder sb = new StringBuilder();
            //当前遍历的对象
            List<TreeNode> list_each = list.Where(x => x.Pid == pid).ToList<TreeNode>();

            int i = 0;
            foreach (TreeNode tn in list_each)
            {
                if (i == 0)//某一层的开始，“[”开头
                    if (deep == 1)
                        sb.Append("[");//深度为1,即第一层  
                    else
                        sb.Append(",\"children\":[");//为第二层或更深  
                else
                    sb.Append(",");

                sb.Append("{");
                sb.AppendFormat("\"id\":\"{0}\",\"text\":\"{1}\",\"pid\":\"{2}\",", tn.Id, tn.Text.OfJson(), tn.Pid);
                sb.AppendFormat("\"ext1\":\"{0}\",\"ext2\":\"{1}\",\"ext3\":\"{2}\"", tn.Ext1.OfJson(), tn.Ext2.OfJson(), tn.Ext3.OfJson());
                List<TreeNode> list_next = list.Where(x => x.Pid != pid).ToList<TreeNode>();
                sb.Append(TreeJson(list_next, tn.Id, deep + 1));
                sb.Append("}");
                i += 1;
                //某一层到了末尾,"]"结束  
                if (list_each.Count == i)
                    sb.Append("]");
            }
            return sb.ToString();
        }


        /// <summary>
        /// 节点实体
        /// </summary>
        public class TreeNode
        {
            private string _id;
            /// <summary>
            /// ID
            /// </summary>
            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }
            private string _pid;
            /// <summary>
            /// 父ID
            /// </summary>
            public string Pid
            {
                get { return _pid; }
                set { _pid = value; }
            }
            private string _text;
            /// <summary>
            /// 文本
            /// </summary>
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }
            private string _ext1;
            /// <summary>
            /// 拓展1
            /// </summary>
            public string Ext1
            {
                get { return _ext1; }
                set { _ext1 = value; }
            }
            private string _ext2;
            /// <summary>
            /// 拓展2
            /// </summary>
            public string Ext2
            {
                get { return _ext2; }
                set { _ext2 = value; }
            }
            private string _ext3;
            /// <summary>
            /// 拓展3
            /// </summary>
            public string Ext3
            {
                get { return _ext3; }
                set { _ext3 = value; }
            }
        }

        #endregion

        #region 导航菜单

        /// <summary>
        /// 获得菜单
        /// </summary>
        /// <param name="type">默认所有菜单</param>
        /// <returns></returns>
        [OutputCache(Duration = 30)]
        public string QueryMenuTree(string type = "all")
        {
            string json = "[]";
            DataTable dt = DAL.PartialDAL.GetSysMenu();
            DataRow drole = DAL.PartialDAL.GetSysRole();
            string wheres = type == "all" ? "" : "id in(" + drole["r_menus"].ToString() + ")";
            var drs = dt.Select(wheres, "m_order");
            if (drs.Length == 0) return json;

            List<TreeNode> list = new List<TreeNode>();
            foreach (DataRow dr in drs)
            {
                TreeNode tn = new TreeNode();

                tn.Id = dr["id"].ToString();
                tn.Pid = dr["m_pid"].ToString();
                tn.Text = dr["m_name"].ToString();
                tn.Ext1 = dr["m_url"].ToString();
                tn.Ext2 = dr["m_icon"].ToString();
                tn.Ext3 = "";
                list.Add(tn);
            }

            json = TreeJson(list, "0", 1);
            return json;
        }


        #endregion


        #region 公用查询 普通表数据

        [HttpPost]
        public string QueryData(QueryInputModel qim)
        {
            //输入参数
            string KeyWords = string.Empty;
            if (!string.IsNullOrEmpty(Request["keywords"]))
            {
                KeyWords = Request["keywords"].ToString().OfSql();
            }

            //构查询实体
            QueryDataSetModel dsm = new QueryDataSetModel();
            dsm.pageIndex = Convert.ToInt32(Request["pageIndex"]);
            dsm.pageSize = Convert.ToInt32(Request["pageSize"]);

            //点击标题排序
            string OrderBy = string.Empty;
            if (Request.Form["sortField"].ToString().Length > 1)
            {
                string[] sortField = Request.Form["sortField"].ToString().Split(',');
                string[] sortOrder = Request.Form["sortOrder"].ToString().Split(',');
                for (int i = 0; i < sortField.Length; i++)
                {
                    OrderBy += "," + sortField[i] + " " + sortOrder[i];
                }
                OrderBy = OrderBy.Substring(1);
            }

            switch (qim.uri.ToLower())
            {
                #region 公共查询

                //表格配置
                case "pptableconfig":
                    dsm.tableName = "view_table_config";
                    dsm.orderBy = "l_order";
                    dsm.whereStr = "vname='" + Request["vname"].ToString().OfSql() + "'";
                    dsm.total = false;
                    break;

                //菜单
                case "ppmenu":
                    dsm.tableName = "sys_menu";
                    dsm.orderBy = OrderBy == "" ? "m_order" : OrderBy;
                    dsm.whereStr = "m_pid=0";
                    dsm.total = true;
                    break;

                #endregion

                #region Setting 控制器

                //角色查询
                case "pprole":
                    dsm.tableName = "sys_role";
                    dsm.orderBy = OrderBy == "" ? "id" : OrderBy;
                    dsm.total = true;
                    break;

                //操作日志
                case "pplog":
                    dsm.tableName = "sys_log";
                    dsm.orderBy = OrderBy == "" ? "l_datetime desc" : OrderBy;
                    dsm.whereStr = KeyWords != "" ? string.Format("l_user like '{0}%' or l_action ='%{0}%' or l_content ='%{0}%' or l_ip ='%{0}%'", KeyWords) : "";
                    dsm.total = true;
                    break;

                //列表配置
                case "ppcolumn":
                    dsm.tableName = "sys_column";
                    dsm.orderBy = OrderBy == "" ? "id" : OrderBy;
                    dsm.whereStr = KeyWords != "" ? string.Format("vname like '{0}%' or field ='{0}'", KeyWords) : "";
                    dsm.total = true;
                    break;

                //按钮列表
                case "ppbutton":
                    dsm.tableName = "sys_button";
                    dsm.orderBy = OrderBy == "" ? "b_pid,b_order" : OrderBy;
                    break;

                //用户查询
                case "ppuser":
                    dsm.tableName = "view_sys_user";
                    dsm.orderBy = OrderBy == "" ? "u_state" : OrderBy;
                    dsm.whereStr = KeyWords != "" ? string.Format("u_name like '%{0}%'", KeyWords) : "";
                    dsm.total = true;
                    break;
                #endregion

            }

            if (dsm.tableName.Length == 0 || dsm.orderBy.Length == 0)
            {
                throw new Exception("查询传参错误，缺失表名或排序列");
            }

            //得到数据集
            DataSet ds = QueryDS(dsm, DBT.sqllite);

            //输出数据
            QueryOutputModel qom = new QueryOutputModel();
            if (dsm.total)
            {
                qom.total = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            }
            qom.data = ds.Tables[0];
            //第一次查询列配置
            if (Convert.ToInt32(Request["columnsExists"]) == 0)
            {
                qom.columns = QueryColumns(dsm.tableName);
            }
            //列配置表名
            qom.vname = dsm.tableName;

            return qom.ToJson();
        }

        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <returns></returns>
        private DataSet QueryDS(QueryDataSetModel dsm, DBT dbt = DBT.sqllite)
        {
            DataSet ds = new DataSet();

            string sql = string.Empty;
            switch (dbt)
            {
                case DBT.sqllite:
                    sql = "SELECT " + dsm.columns + " FROM " + dsm.tableName + " WHERE " + dsm.whereStr + " ORDER BY " + dsm.orderBy + " LIMIT " + dsm.pageSize + " OFFSET " + dsm.pageSize * (dsm.pageIndex - 1);
                    if (dsm.total)
                    {
                        sql += "; select count(1) AS counts from " + dsm.tableName + " WHERE " + dsm.whereStr;
                        ds = DB.HelperSQLite.Query(sql);
                    }
                    else
                    {
                        ds = DB.HelperSQLite.Query(sql);
                    }
                    break;
            }

            return ds;
        }


        /// <summary>
        /// 公用查询 输入
        /// </summary>
        public class QueryInputModel
        {
            private string _uri;
            /// <summary>
            /// 请求标识
            /// </summary>
            public string uri
            {
                get { return _uri; }
                set { _uri = value; }
            }

            private string _parameter;
            /// <summary>
            /// 参数 JSON字符串
            /// </summary>
            public string parameter
            {
                get { return _parameter; }
                set { _parameter = value; }
            }
        }

        /// <summary>
        /// 公用查询 输出 可根据需求添加输出项
        /// </summary>
        public class QueryOutputModel
        {
            private int _total = 0;
            /// <summary>
            /// 总条数
            /// </summary>
            public int total
            {
                get { return _total; }
                set { _total = value; }
            }

            private DataTable _columns;
            /// <summary>
            /// 列表配置
            /// </summary>
            public DataTable columns
            {
                get { return _columns; }
                set { _columns = value; }
            }

            private DataTable _data;
            /// <summary>
            /// 数据源
            /// </summary>
            public DataTable data
            {
                get { return _data; }
                set { _data = value; }
            }
            private string _vname;
            /// <summary>
            /// 列配置表名
            /// </summary>
            public string vname
            {
                get { return _vname; }
                set { _vname = value; }
            }
        }

        /// <summary>
        /// 公用查询 查询实体
        /// </summary>
        private class QueryDataSetModel
        {
            private int _pageIndex = 1;
            /// <summary>
            /// 页码 默认1
            /// </summary>
            public int pageIndex
            {
                get { return _pageIndex; }
                set { _pageIndex = value; }
            }

            private int _pageSize = 30;
            /// <summary>
            /// 页量 默认30
            /// </summary>
            public int pageSize
            {
                get { return _pageSize; }
                set { _pageSize = value; }
            }

            private string _columns = "*";
            /// <summary>
            /// 查询列名 默认*
            /// </summary>
            public string columns
            {
                get { return _columns; }
                set { _columns = value; }
            }

            private string _tableName;
            /// <summary>
            /// 表名
            /// </summary>
            public string tableName
            {
                get { return _tableName; }
                set { _tableName = value; }
            }

            private string _whereStr = "1=1";
            /// <summary>
            /// 查询条件 注意 SQL 注入
            /// </summary>
            public string whereStr
            {
                get
                {
                    if (_whereStr.Trim() == "")
                    {
                        _whereStr = "1=1";
                    }
                    return _whereStr;
                }
                set { _whereStr = value; }
            }

            private string _orderBy;
            /// <summary>
            /// 排序
            /// </summary>
            public string orderBy
            {
                get { return _orderBy; }
                set { _orderBy = value; }
            }

            private bool _total = false;
            /// <summary>
            /// 是否查询总条数 默认否
            /// </summary>
            public bool total
            {
                get { return _total; }
                set { _total = value; }
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        private enum DBT
        {
            mssql, oracle, mysql, sqllite
        }

        #endregion

        #region 列表配置 查询

        /// <summary>
        /// 列表配置 查询
        /// </summary>
        /// <param name="vname">虚拟表名</param>
        /// <returns></returns>
        public DataTable QueryColumns(string vname)
        {
            DataTable dt = DB.HelperSQLite.Query("select * from sys_column where vname='" + vname.Replace("'", "") + "' and IFNULL(hide,'')!=1 order by l_order ASC").Tables[0];
            return dt;
        }

        #endregion


        #region 角色列表 Combobox

        public string QuerySysRoleBox()
        {
            string sql = "select id,r_name as text from sys_role where r_state=1";
            DataTable dt = DB.HelperSQLite.Query(sql).Tables[0];
            return dt.ToJson();
        }

        #endregion
    }
}
