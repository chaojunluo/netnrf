using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Netnr.Data;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    public class IOController : Controller
    {
        /// <summary>
        /// 公共导出
        /// 默认查询所有数据 再根据表配置保留要导出的列
        /// 
        /// </summary>
        [Description("公共导出")]
        public void Export(QueryDataVM.GetParams param)
        {
            if (!string.IsNullOrWhiteSpace(param.uri))
            {
                //根据uri转换表名，不建议直接传表名（有些表涉密不允许导出）
                string tablename = "";
                var listtb = new List<string>()
                {
                    "syslog",
                    "systableconfig",
                    "sysuser"
                };
                tablename = listtb.Where(x => x.ToLower() == param.uri).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(tablename))
                {
                    return;
                }

                //导出文件名
                string filename = "导出表格";

                //生成excel的临时文件目录
                string path = Server.MapPath("/temp/");

                //存放导出的数据
                var dt = new DataTable();

                //查询导出的数据
                string sql = ExportSql(tablename);

                #region 表配置处理（列排序、列改名、剔除不要的列）
                using (var db = new ContextBase())
                {
                    var configquery = from a in db.SysTableConfig
                                      where a.TableName == param.tableName && a.ColHide == 0 && a.ColExport == 1
                                      orderby (a.ColFrozen ?? 0) descending, a.ColOrder ascending
                                      select new
                                      {
                                          a.ColField,
                                          a.ColTitle
                                      };
                    //配置信息
                    var configlist = configquery.ToList();
                    var listcol = configlist.Select(x => x.ColField.ToLower()).ToList();

                    using (var conn = db.Database.Connection)
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        dt.Load(cmd.ExecuteReader());
                    }
                    //清空表格主键（不允许移除主键列）
                    dt.PrimaryKey = null;

                    //需要删除的列
                    var listcolRemove = new List<string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (!listcol.Contains(dc.ColumnName.ToLower()))
                        {
                            listcolRemove.Add(dc.ColumnName);
                        }
                    }
                    //删除列
                    foreach (var item in listcolRemove)
                    {
                        dt.Columns.Remove(item);
                    }

                    //列排序、重命名
                    for (int i = 0; i < configlist.Count; i++)
                    {
                        var item = configlist[i];
                        if (dt.Columns.Contains(item.ColField))
                        {
                            var dc = dt.Columns[item.ColField];
                            //列排序
                            dc.SetOrdinal(i);

                            //重命名列
                            int index = 1;
                            string newcol = item.ColTitle;
                            while (dt.Columns.Contains(newcol))
                            {
                                newcol = item.ColTitle + index.ToString();
                                index++;
                            }
                            dc.ColumnName = newcol;
                        }
                    }

                }
                #endregion

                #region 生成Excel并输出流
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filename += DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (Fast.NpoiTo.DataTableToExcel(dt, path + filename))
                {
                    Core.DownTo.Stream(path, filename);
                }
                #endregion
            }
        }

        /// <summary>
        /// 根据表名处理导出的SQL语句（多表关联查询、格式化、排序等）
        /// </summary>
        /// <param name="tablename">表名，小写</param>
        /// <returns></returns>
        private string ExportSql(string tablename)
        {
            string sql = "select * from " + tablename;

            switch (tablename)
            {
                #region 角色表
                case "sysrole":
                    {
                        sql = "select * from sysrole order by createtime";
                    }
                    break;
                    #endregion
            }

            return sql;
        }

        /// <summary>
        /// 公共格式化
        /// </summary>
        /// <param name="field">case字段</param>
        /// <param name="type">格式化类型</param>
        /// <param name="asField">别名,（默认caseField，自动去掉表名点）</param>
        /// <returns></returns>
        private string FormatColumn(string field, int type, string asField = null)
        {
            string result = string.Empty;

            switch (type)
            {
                //1√
                case 1:
                    result += "CASE " + field + " WHEN 1 THEN '√' ELSE '×' END";
                    break;

                //1×
                case 2:
                    result += "CASE " + field + " WHEN 1 THEN '×' ELSE '√' END";
                    break;
            }

            if (asField == null)
            {
                asField = field.Split('.').LastOrDefault();
            }
            result += " AS " + asField + " ";

            return result;
        }
    }
}