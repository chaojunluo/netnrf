using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 工具
    /// </summary>
    [Route("[controller]/[action]")]
    public class ToolController : Controller
    {
        #region 表管理

        /// <summary>
        /// 表管理
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult TableManage()
        {
            using var db = new ContextBase();
            if (ContextBase.TDB == ContextBase.TypeDB.InMemory)
            {
                return Content("不支持内存数据库，请切换其它数据库");
            }

            return View();
        }

        /// <summary>
        /// 获取scripts
        /// </summary>
        /// <param name="typedb">数据库类型</param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpGet]
        public string QueryScripts(string typedb, string cmd)
        {
            string ext = ".sql";
            if (cmd == "pd")
            {
                ext = ".pdm";
            }
            var sql = Core.FileTo.ReadText(GlobalTo.WebRootPath + $"/scripts/table-{cmd}/", typedb.ToLower() + ext);
            return sql;
        }

        /// <summary>
        /// 查询数据库表与表配置信息
        /// </summary>
        /// <param name="ivm"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public QueryDataOutputVM QueryTableConfig(QueryDataInputVM ivm)
        {
            var ovm = new QueryDataOutputVM();

            using (var db = new ContextBase())
            {
                var queryHas = from a in db.SysTableConfig
                               group a by a.TableName into g
                               select g.Key;

                var listHas = queryHas.ToList();

                var dbname = db.Database.GetDbConnection().Database;

                var sql = QueryScripts(ContextBase.TDB.ToString(), "name").Replace("@DataBaseName", dbname);

                var listRow = new List<TreeNodeVM>();
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    using var dt = new DataTable();

                    using (var conn = db.Database.GetDbConnection())
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        dt.Load(cmd.ExecuteReader());
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        var key = dr[0].ToString();
                        var val = (listHas.Exists(x => x.ToLower() == key.ToLower()) ? "1" : "");
                        var row = new TreeNodeVM
                        {
                            id = key,
                            pid = val
                        };
                        listRow.Add(row);
                    }
                }

                var listFilter = Func.Common.QueryWhere(listRow, ivm);
                ovm.data = listFilter;
                ovm.total = listFilter.Count();
            }

            return ovm;
        }

        /// <summary>
        /// 建立表配置信息
        /// </summary>
        /// <param name="names">表名，逗号分割</param>
        /// <param name="maketype">1追加不存在的列，2覆盖</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResultVM BuildTableConfig(string names, int maketype)
        {
            var vm = new ActionResultVM();

            var listName = names.Split(',');
            if (string.IsNullOrWhiteSpace(names) || listName.Length == 0)
            {
                vm.code = 1;
                vm.msg = "表名不能为空";
            }
            if (ContextBase.TDB == ContextBase.TypeDB.SQLite)
            {
                vm.Set(ARTag.lack);
                vm.msg = "不支持当前数据库";
            }
            else
            {
                using var db = new ContextBase();
                var dbname = db.Database.GetDbConnection().Database;
                var sqltemplate = QueryScripts(ContextBase.TDB.ToString(), "config");

                foreach (var name in listName)
                {
                    var sql = sqltemplate.Replace("@DataBaseName", dbname).Replace("@TableName", name);

                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        var dt = new DataTable();

                        using (var dbsql = new ContextBase())
                        {
                            using var conn = dbsql.Database.GetDbConnection();
                            conn.Open();
                            var cmd = conn.CreateCommand();
                            cmd.CommandText = sql;
                            dt.Load(cmd.ExecuteReader());
                        }

                        if (dt.Rows.Count > 0)
                        {
                            switch (maketype)
                            {
                                case 1:
                                    {
                                        var listField = db.SysTableConfig
                                            .Where(x => listName.Contains(x.TableName))
                                            .Select(x => new { x.TableName, x.ColField }).ToList();

                                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                                        {
                                            string tbname = dt.Rows[i]["TableName"].ToString();
                                            string field = dt.Rows[i]["ColField"].ToString();
                                            var hasRow = listField.Where(x => x.TableName == tbname && x.ColField == field).ToList();
                                            if (hasRow.Count > 0)
                                            {
                                                dt.Rows.RemoveAt(i);
                                            }
                                        }
                                    }
                                    break;
                                case 2:
                                    {
                                        var delstc = db.SysTableConfig.Where(x => x.TableName == name).ToList();
                                        db.SysTableConfig.RemoveRange(delstc);
                                    }
                                    break;
                            }

                            var listMo = dt.ToModel<SysTableConfig>();
                            //主键统一用程序生成GUID覆盖
                            listMo.ForEach(x => x.Id = Guid.NewGuid().ToString());
                            db.SysTableConfig.AddRange(listMo);

                            db.SaveChanges();
                        }
                    }
                }

                vm.Set(ARTag.success);
            }

            return vm;
        }

        /// <summary>
        /// 补齐表配置信息
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public ActionResultVM RepairTableConfig(string names)
        {
            var vm = new ActionResultVM();

            var listName = names.Split(',');
            if (string.IsNullOrWhiteSpace(names) || listName.Length == 0)
            {
                vm.code = 1;
                vm.msg = "表名不能为空";
            }
            else
            {
                using var db = new ContextBase();
                var dbname = db.Database.GetDbConnection().Database;
                var sqltemplate = QueryScripts(ContextBase.TDB.ToString(), "config");

                var listTc = db.SysTableConfig.ToList();

                foreach (var name in listName)
                {
                    var sql = sqltemplate.Replace("@DataBaseName", dbname).Replace("@TableName", name);

                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        var dt = new DataTable();

                        using (var dbsql = new ContextBase())
                        {
                            using var conn = dbsql.Database.GetDbConnection();
                            conn.Open();
                            var cmd = conn.CreateCommand();
                            cmd.CommandText = sql;
                            dt.Load(cmd.ExecuteReader());
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            var tcmo = listTc.Where(x => x.TableName == dr["TableName"].ToString() && x.ColField == dr["ColField"].ToString()).FirstOrDefault();
                            tcmo.FormMaxlength = Convert.ToInt32(dr["FormMaxlength"]);
                            if (string.IsNullOrWhiteSpace(tcmo.ColRelation))
                            {
                                tcmo.ColRelation = "Equal,Contains";
                            }
                        }
                    }
                }

                db.SysTableConfig.UpdateRange(listTc);
                db.SaveChanges();

                vm.Set(ARTag.success);
            }

            return vm;
        }

        /// <summary>
        /// 查询数据库表信息
        /// </summary>
        /// <param name="names">表名，逗号分割</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public QueryDataOutputVM QueryTableInfo(string names)
        {
            var listName = names.Split(',').ToList();
            var innames = string.Join("','", listName);

            var ovm = new QueryDataOutputVM();

            var dt = new DataTable();

            using (var db = new ContextBase())
            {
                var dbname = db.Database.GetDbConnection().Database;

                if (ContextBase.TDB == ContextBase.TypeDB.SQLite)
                {
                    //补齐列
                    dt.Columns.Add(new DataColumn("表名"));
                    dt.Columns.Add(new DataColumn("表说明"));

                    using var conn = db.Database.GetDbConnection();
                    conn.Open();
                    //遍历表查询
                    foreach (var tname in listName)
                    {
                        var sql = QueryScripts(ContextBase.TDB.ToString(), "info").Replace("@TableName", tname);

                        if (!string.IsNullOrWhiteSpace(sql))
                        {
                            var cmd = conn.CreateCommand();
                            cmd.CommandText = sql;

                            using var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                var newdr = dt.NewRow();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var field = reader.GetName(i);
                                    if (!dt.Columns.Contains(field))
                                    {
                                        dt.Columns.Add(new DataColumn(field));
                                    }

                                    newdr[field] = reader[field].ToString();

                                    switch (field)
                                    {
                                        case "notnull":
                                            newdr[field] = newdr[field].ToString() == "1" ? "YES" : "";
                                            break;
                                        case "pk":
                                            newdr[field] = newdr[field].ToString() == "0" ? "" : "YES (" + newdr[field].ToString() + ")";
                                            break;
                                    }
                                }

                                newdr["表名"] = tname;
                                newdr["表说明"] = "";

                                dt.Rows.Add(newdr.ItemArray);
                            }
                        }
                    }

                    //更改列名
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == "name")
                        {
                            dc.ColumnName = "字段名";
                        }
                        if (dc.ColumnName == "type")
                        {
                            dc.ColumnName = "类型";
                        }
                        if (dc.ColumnName == "notnull")
                        {
                            dc.ColumnName = "不为空";
                        }
                        if (dc.ColumnName == "dflt_value")
                        {
                            dc.ColumnName = "默认值";
                        }
                        if (dc.ColumnName == "pk")
                        {
                            dc.ColumnName = "主键";
                        }
                    }
                }
                else
                {
                    var sql = QueryScripts(ContextBase.TDB.ToString(), "info").Replace("@DataBaseName", dbname).Replace("@TableName", innames);

                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        using var conn = db.Database.GetDbConnection();
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        dt.Load(cmd.ExecuteReader());
                    }
                }

                ovm.data = dt;
                ovm.total = dt.Rows.Count;
            }

            #region 其它处理
            switch (ContextBase.TDB)
            {
                //mysql默认值，单独查询
                case ContextBase.TypeDB.MySQL:
                    using (var db = new ContextBase())
                    {
                        var conn = db.Database.GetDbConnection();
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        foreach (var name in listName)
                        {
                            cmd.CommandText = "desc " + name + ";";

                            var dtdefault = new DataTable();
                            dtdefault.Load(cmd.ExecuteReader());

                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dr["表名"].ToString().ToLower() == name.ToLower())
                                {
                                    var dv = dtdefault.Select("Field='" + dr["字段名"].ToString() + "'")[0]["Default"];
                                    if (dv != DBNull.Value)
                                    {
                                        dr["默认值"] = dv;
                                    }
                                }
                            }
                        }
                        cmd.Dispose();
                        conn.Dispose();
                    }
                    break;
            }
            #endregion

            return ovm;
        }

        /// <summary>
        /// 导出表设计
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResultVM ExportTableInfo(string names)
        {
            var vm = new ActionResultVM();

            var dt = QueryTableInfo(names).data as DataTable;

            if (dt.Rows.Count > 0)
            {
                dt.PrimaryKey = null;

                var drname = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];

                    string currname = dr[0].ToString();
                    string currdescription = dr[1].ToString();

                    if (string.IsNullOrWhiteSpace(drname))
                    {
                        drname = currname;
                    }

                    if (drname != currname || i == 0)
                    {
                        drname = currname;
                        string drdescription = currdescription;
                        var newdr = dt.NewRow();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            newdr[dc.ColumnName] = string.Empty;
                        }
                        newdr[2] = "【" + drname + "】" + drdescription;

                        dt.Rows.InsertAt(newdr, i);
                        i++;

                        drname = currname;
                    }
                }

                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(0);

                var path = "/upload/temp/";
                var vpath = GlobalTo.WebRootPath + path;

                var filename = "数据库表设计_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                Fast.NpoiTo.DataTableToExcel(dt, vpath + filename);

                //基于导出的Excel绘制，加粗、合并等操作
                Func.ExportAid.ExcelDraw(vpath + filename, new QueryDataInputVM()
                {
                    tableName = "DatabaseTableDesign"
                });

                vm.Set(ARTag.success);
                vm.data = path + filename;
            }

            return vm;
        }

        /// <summary>
        /// 保存生成的代码
        /// </summary>
        /// <param name="name">视图名称</param>
        /// <param name="controller">控制器名称</param>
        /// <param name="view">视图内容</param>
        /// <param name="javascript">视图页面脚本内容</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResultVM SaveGenerateCode(string name, string controller, string view, string javascript)
        {
            var vm = new ActionResultVM();

            try
            {
                var rootg = "/upload/temp/.ignore/GenerateCode/";
                var rootc = GlobalTo.WebRootPath + rootg + "Controllers/";
                var rootv = GlobalTo.WebRootPath + rootg + "Views/" + name + "/";
                var rootj = GlobalTo.WebRootPath + rootg + "js/" + name.ToLower() + "/";

                if (!Directory.Exists(rootc))
                {
                    Directory.CreateDirectory(rootc);
                }
                if (!Directory.Exists(rootv))
                {
                    Directory.CreateDirectory(rootv);
                }
                if (!Directory.Exists(rootj))
                {
                    Directory.CreateDirectory(rootj);
                }

                Core.FileTo.WriteText(controller, rootc, name + ".cs", false);
                Core.FileTo.WriteText(view, rootv, name + ".cshtml", false);
                Core.FileTo.WriteText(javascript, rootj, name.ToLower() + ".js", false);

                vm.Set(ARTag.success);
            }
            catch (Exception ex)
            {
                vm.Set(ex);
            }

            return vm;
        }

        /// <summary>
        /// 根据JSON数据重置数据库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResultVM ResetDataBaseForJson()
        {
            var vm = new ActionResultVM();

            try
            {
                int num = new Func.DataMirrorAid().AddForJson();
                vm.Set(num > 0);
                vm.data = num;
            }
            catch (Exception ex)
            {
                vm.Set(ex);
            }

            return vm;
        }

        /// <summary>
        /// 数据库备份数据为JSON
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResultVM BackupDataBaseAsJson()
        {
            var vm = new ActionResultVM();

            try
            {
                //是否覆盖JSON文件，默认不覆盖，避免线上重置功能被破坏
                var CoverJson = false;

                vm = new Func.DataMirrorAid().SaveAsJson(CoverJson);
            }
            catch (Exception ex)
            {
                vm.Set(ex);
            }

            return vm;
        }

        #endregion
    }
}