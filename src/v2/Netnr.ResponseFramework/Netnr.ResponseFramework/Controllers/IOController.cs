using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class IOController : Controller
    {
        /// <summary>
        /// 公共导出
        /// 默认查询所有数据 再根据表配置保留要导出的列
        /// 
        /// </summary>
        public void Export(QueryDataVM.GetParams param)
        {
            if (Func.Common.DicTable.ContainsKey(param.uri))
            {
                //表名
                param.tableName = Func.Common.DicTable[param.uri];

                //导出文件名
                string filename = "导出表格";

                //生成excel的临时文件目录
                string path = GlobalVar.WebRootPath + "/temp/";

                //存放导出的数据
                var dt = new DataTable();

                //查询导出的数据
                string sql = ExportSql(param.tableName.ToLower());
                dt = SQLServerDB.GetDataTable(sql);

                #region 表配置处理（列排序、列改名、剔除不要的列）
                using (var ru = new RepositoryUse())
                {
                    var configquery = from a in ru.Context.Set<Domain.SysTableConfig>()
                                      where a.TableName == param.tableName && a.ColHide == 0 && a.ColExport == 1
                                      orderby (a.ColFrozen == null ? 0 : a.ColFrozen) descending, a.ColOrder ascending
                                      select new
                                      {
                                          a.ColField,
                                          a.ColTitle
                                      };
                    //配置信息
                    var configlist = configquery.ToList();

                    var listcol = configlist.Select(x => x.ColField.ToLower()).ToList();
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
                if (Core.NPOITo.DataTableToExcel(dt, path + filename))
                {
                    new Core.DownTo(Response).Stream(path, filename);
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
                #region 用户表
                case "sysuser":
                    {
                        //RoleId是角色表name字段的别名
                        //Status状态调用公共格式化方法
                        sql = "SELECT b.Name AS RoleID,UserName,Nickname," + FormatColumn("a.CreateTime", 4) + "," + FormatColumn("a.status", 1) + " FROM SysUser a LEFT JOIN SysRole b ON a.RoleID = b.ID;";
                    }
                    break;
                #endregion

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

                //yyyy-MM-dd HH:mm:ss
                case 3:
                    result += "CONVERT(VARCHAR(19), " + field + ", 120)";
                    break;

                //yyyy-MM-dd
                case 4:
                    result += "CONVERT(VARCHAR(10), " + field + ", 120)";
                    break;

                //HH:mm:ss
                case 5:
                    result += "CONVERT(VARCHAR(8), " + field + ", 108)";
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