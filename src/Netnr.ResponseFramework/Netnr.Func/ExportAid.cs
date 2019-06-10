using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Netnr.Func
{
    /// <summary>
    /// 导出辅助
    /// </summary>
    public class ExportAid
    {
        /// <summary>
        /// 查询拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="db"></param>
        /// <param name="listColumn">表配置</param>
        /// <returns></returns>
        public static List<T> QueryJoin<T>(IQueryable<T> query, QueryDataVM.GetParams param, ContextBase db, out List<SysTableConfig> listColumn)
        {
            //条件
            query = Common.QueryWhere(query, param);

            //排序
            if (!string.IsNullOrWhiteSpace(param.sort))
            {
                query = Fast.QueryableTo.OrderBy(query, param.sort, param.order);
            }

            //数据
            List<T> list = query.ToList();

            //列
            listColumn = db.SysTableConfig.Where(x => x.TableName == param.uri).OrderBy(x => x.ColOrder).ToList();

            return list;
        }

        /// <summary>
        /// 数据实体映射
        /// </summary>
        public static DataTable ModelsMapping<T>(string tableName, List<T> listModels, List<SysTableConfig> listColumn)
        {
            //转表（类型为字符串）
            DataTable dt = ToDataTableForString(listModels);

            //更改列长度
            foreach (DataColumn col in dt.Columns)
            {
                col.MaxLength = short.MaxValue * 9;
            }

            //调整列排序
            var colorder = listColumn.Where(x => dt.Columns.Contains(x.ColField)).OrderBy(x => x.ColOrder).ToList();
            for (int i = 0; i < colorder.Count; i++)
            {
                var ci = colorder[i];
                if (dt.Columns.Contains(ci.ColField))
                {
                    dt.Columns[ci.ColField].SetOrdinal(i);
                }
            }

            #region 单元格转换
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string field = dt.Columns[i].ColumnName;
                    dr[i] = CellFormat(tableName, field, dr[i].ToString(), dr);
                }
            }
            #endregion

            //剔除不导出的列
            List<SysTableConfig> removeCol = listColumn.Where(x => x.ColExport != 1).ToList();
            foreach (SysTableConfig col in removeCol)
            {
                if (dt.Columns.Contains(col.ColField))
                {
                    dt.Columns.Remove(dt.Columns[col.ColField]);
                }
            }
            //剔除没在表配置的列
            List<string> removeColNotExists = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (listColumn.Where(x => x.ColField == dc.ColumnName).Count() == 0)
                {
                    removeColNotExists.Add(dc.ColumnName);
                }
            }
            foreach (string col in removeColNotExists)
            {
                dt.Columns.Remove(dt.Columns[col]);
            }

            //更改列名为中文（重复的列，后面追加4位随机数）
            foreach (SysTableConfig col in listColumn)
            {
                if (dt.Columns.Contains(col.ColField))
                {
                    try
                    {
                        dt.Columns[col.ColField].ColumnName = col.ColTitle;
                    }
                    catch (Exception)
                    {
                        dt.Columns[col.ColField].ColumnName = col.ColTitle + "-" + Core.RandomTo.NumCode();
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 单元格格式化
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="value">单元格值</param>
        /// <param name="dr">当前行</param>
        /// <returns></returns>
        public static string CellFormat(string tableName, string field, string value, DataRow dr)
        {
            //格式化后的值
            string result = value;

            try
            {
                switch (tableName.ToLower())
                {
                    //角色
                    case "sysrole":
                        {
                            switch (field.ToLower())
                            {
                                //时间
                                case "srcreatetime":
                                    result = CellMapping.DateTimeFormat(value);
                                    break;

                                //状态
                                case "srstatus":
                                    result = CellMapping.Status01(value);
                                    break;
                            }
                        }
                        break;

                    //用户
                    case "sysuser":
                        {
                            switch (field.ToLower())
                            {
                                //角色ID
                                case "srid":
                                    result = dr["SrName"].ToString();
                                    break;

                                //时间
                                case "sucreatetime":
                                    result = CellMapping.DateTimeFormat(value, "yyyy-MM-dd HH:mm:ss");
                                    break;

                                //状态
                                case "sustatus":
                                    result = CellMapping.Status01(value);
                                    break;
                            }
                        }
                        break;

                    //日志
                    case "syslog":
                        {
                            switch (field.ToLower())
                            {
                                //时间
                                case "logcreatetime":
                                    result = CellMapping.DateTimeFormat(value, "yyyy-MM-dd HH:mm:ss");
                                    break;
                            }
                        }
                        break;
                }
            }
            catch (Exception)
            {

            }

            return result;
        }

        /// <summary>
        /// 映射
        /// </summary>
        public class CellMapping
        {
            /// <summary>
            /// 缓存前缀
            /// </summary>
            public static string Mck = "CellMapping_";

            /// <summary>
            /// 缓存过期时间（单位：秒）
            /// </summary>
            public static int Mce = 300;

            /// <summary>
            /// 字典映射转换
            /// </summary>
            /// <param name="kv">格式：1:未生成,2:已生成</param>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string KeyValueMap(string kv, string key)
            {
                var result = key;
                var kvs = kv.Split(',').ToList();
                foreach (var item in kvs)
                {
                    var ims = item.Split(':');
                    if (ims[0] == key)
                    {
                        result = ims[1];
                    }
                }

                return result;
            }

            /// <summary>
            /// 状态格式化
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static string Status01(string value)
            {
                var kv = "1:✔,0:✘";
                return KeyValueMap(kv, value);
            }

            /// <summary>
            /// 状态格式化
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static string Status02(string value)
            {
                var kv = "1:✘,0:✔";
                return KeyValueMap(kv, value);
            }

            /// <summary>
            /// 状态格式化
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static string Status03(string value)
            {
                var kv = "-1:删除,1:正常";
                return KeyValueMap(kv, value);
            }

            /// <summary>
            /// 时间格式化
            /// </summary>
            /// <param name="value"></param>
            /// <param name="format"></param>
            /// <returns></returns>
            public static string DateTimeFormat(string value, string format = "yyyy-MM-dd")
            {
                if (DateTime.TryParse(value, out DateTime dt))
                {
                    value = dt.ToString(format);
                }
                return value;
            }
        }

        /// <summary>
        /// 操作已经生成的Excel
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool ExcelDraw(string fullPath, string uri)
        {
            //需要绘制的记录
            var needDraw = "DatabaseTableDesign,syslog".ToLower().Split(',');
            if (!needDraw.Contains(uri.ToLower()))
            {
                return true;
            }

            string strExtName = Path.GetExtension(fullPath);

            IWorkbook workbook = null;

            using (FileStream file = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (strExtName.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(file);
                }
                if (strExtName.Equals(".xlsx"))
                {
                    workbook = new XSSFWorkbook(file);
                }
            }

            //单元格样式
            ICellStyle cellStyle = workbook.CreateCellStyle();

            //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            ISheet sheet = workbook.GetSheetAt(workbook.ActiveSheetIndex);

            switch (uri.ToLower())
            {
                //数据库表设计
                case "databasetabledesign":
                    {
                        //冻结首行首列
                        sheet.CreateFreezePane(0, 1);

                        var rows = sheet.GetRowEnumerator();

                        while (rows.MoveNext())
                        {
                            IRow row;
                            if (fullPath.Contains(".xlsx"))
                            {
                                row = (XSSFRow)rows.Current;
                            }
                            else
                            {
                                row = (HSSFRow)rows.Current;
                            }

                            var cc = row.GetCell(1);
                            if (string.IsNullOrWhiteSpace(cc.StringCellValue))
                            {
                                //合并
                                sheet.AddMergedRegion(new CellRangeAddress(row.RowNum, row.RowNum, 0, row.Cells.Count - 1));

                                //单元格样式
                                ICellStyle cStyle = workbook.CreateCellStyle();

                                //为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
                                cStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                                cStyle.BorderBottom = BorderStyle.Thin;
                                cStyle.BorderLeft = BorderStyle.Thin;
                                cStyle.BorderRight = BorderStyle.Thin;
                                cStyle.BorderTop = BorderStyle.Thin;
                                cStyle.VerticalAlignment = VerticalAlignment.Center;
                                cStyle.Alignment = HorizontalAlignment.Justify;
                                row.Height = 20 * 25;

                                //单元格字体
                                IFont font = workbook.CreateFont();
                                font.FontName = "宋体";
                                font.FontHeightInPoints = 10;
                                font.Color = 8;
                                font.Boldweight = (short)FontBoldWeight.Bold;

                                cStyle.SetFont(font);

                                cc.CellStyle = cStyle;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            using (FileStream file = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                workbook.Write(file);
                workbook.Close();
            }
            return false;
        }

        /// <summary>
        /// 实体转表，类型为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTableForString<T>(List<T> list)
        {
            Type elementType = typeof(T);
            var t = new DataTable();
            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, typeof(string)));
            foreach (T item in list)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            return t;
        }
    }
}
