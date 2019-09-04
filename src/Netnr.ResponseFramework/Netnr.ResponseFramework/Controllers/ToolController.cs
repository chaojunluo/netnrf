using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class ToolController : Controller
    {
        #region 表管理

        [Description("表管理")]
        public IActionResult TableManage()
        {
            return View();
        }

        [Description("获取scripts")]
        public string QueryScripts(string typedb, string cmd)
        {
            string ext = ".sql";
            if (cmd == "pd")
            {
                ext = ".pdm";
            }
            var sql = Core.FileTo.ReadText(Core.MapPathTo.Map($"/scripts/table-{cmd}/", GlobalTo.HostingEnvironment), typedb.ToLower() + ext);
            return sql;
        }

        [Description("查询数据库表与表配置信息")]
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

                var sql = QueryScripts(db.TDB.ToString(), "name").Replace("@DataBaseName", dbname);

                var dt = new DataTable();

                var listRow = new List<TreeNodeVM>();
                if (!string.IsNullOrWhiteSpace(sql))
                {
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
        [Description("建立表配置信息")]
        public ActionResultVM BuildTableConfig(string names, int maketype)
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
                using (var db = new ContextBase())
                {
                    var dbname = db.Database.GetDbConnection().Database;
                    var sqltemplate = QueryScripts(db.TDB.ToString(), "config");

                    foreach (var name in listName)
                    {
                        var sql = sqltemplate.Replace("@DataBaseName", dbname).Replace("@TableName", name);

                        if (!string.IsNullOrWhiteSpace(sql))
                        {
                            var dt = new DataTable();

                            using (var dbsql = new ContextBase())
                            {
                                using (var conn = dbsql.Database.GetDbConnection())
                                {
                                    conn.Open();
                                    var cmd = conn.CreateCommand();
                                    cmd.CommandText = sql;
                                    dt.Load(cmd.ExecuteReader());
                                }
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
                                listMo.ForEach(x => x.Id = Guid.NewGuid().ToString());
                                db.SysTableConfig.AddRange(listMo);

                                db.SaveChanges();
                            }
                        }
                    }

                    vm.Set(ARTag.success);
                }
            }

            return vm;
        }

        [Description("补齐表配置信息")]
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
                using (var db = new ContextBase())
                {
                    var dbname = db.Database.GetDbConnection().Database;
                    var sqltemplate = QueryScripts(db.TDB.ToString(), "config");

                    var listTc = db.SysTableConfig.ToList();

                    foreach (var name in listName)
                    {
                        var sql = sqltemplate.Replace("@DataBaseName", dbname).Replace("@TableName", name);

                        if (!string.IsNullOrWhiteSpace(sql))
                        {
                            var dt = new DataTable();

                            using (var dbsql = new ContextBase())
                            {
                                using (var conn = dbsql.Database.GetDbConnection())
                                {
                                    conn.Open();
                                    var cmd = conn.CreateCommand();
                                    cmd.CommandText = sql;
                                    dt.Load(cmd.ExecuteReader());
                                }
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
            }

            return vm;
        }

        /// <summary>
        /// 查询数据库表信息
        /// </summary>
        /// <param name="names">表名，逗号分割</param>
        /// <returns></returns>
        [Description("查询数据库表信息")]
        public QueryDataOutputVM QueryTableInfo(string names)
        {
            var listName = names.Split(',').ToList();
            var innames = string.Join("','", listName);

            var ovm = new QueryDataOutputVM();

            var dt = new DataTable();

            ContextBase.TypeDB tdb;
            using (var db = new ContextBase())
            {
                tdb = db.TDB;

                var dbname = db.Database.GetDbConnection().Database;

                var sql = QueryScripts(db.TDB.ToString(), "info").Replace("@DataBaseName", dbname).Replace("@TableName", innames);

                if (!string.IsNullOrWhiteSpace(sql))
                {
                    using (var conn = db.Database.GetDbConnection())
                    {
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
            //mysql默认值，单独查询
            switch (tdb)
            {
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

        [Description("导出表设计")]
        public ActionResultVM ExportTableInfo(string names)
        {
            var vm = new ActionResultVM();

            var listName = names.Split(',').ToList();
            var innames = string.Join("','", listName);

            var dt = new DataTable();

            using (var db = new ContextBase())
            {
                var dbname = db.Database.GetDbConnection().Database;

                var sql = QueryScripts(db.TDB.ToString(), "info").Replace("@DataBaseName", dbname).Replace("@TableName", innames);

                if (!string.IsNullOrWhiteSpace(sql))
                {
                    using (var conn = db.Database.GetDbConnection())
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        dt.Load(cmd.ExecuteReader());
                    }
                }
            }

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
                var vpath = (GlobalTo.WebRootPath + path).Replace("\\", "/");

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

        [Description("保存生成的代码")]
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

        [Description("重置数据库")]
        public ActionResultVM ResetDataBase()
        {
            var vm = new ActionResultVM();

            using (var db = new ContextBase())
            {
                string sql = QueryScripts(db.TDB.ToString(), "reset");
                using (var conn = db.Database.GetDbConnection())
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    int num = cmd.ExecuteNonQuery();

                    vm.Set(ARTag.success);
                    vm.data = "受影响行数：" + num;
                }
            }

            return vm;
        }

        #endregion
    }
}