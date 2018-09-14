using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netnr.Data;
using Netnr.Domain;

namespace Netnr.ResponseFramework.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return Content("Tool");
        }

        #region SQLServer 快速生成表配置
        public IActionResult SQLServer()
        {
            return View();
        }

        private class SQLServerSysObjects
        {
            public string name { get; set; }
        }

        public string SQLServerTableInfo()
        {
            string result = string.Empty;
            var listAll = new List<string>();
            var listHas = new List<string>();
            using (var ru = new RepositoryUse())
            {
                var queryHas = from a in ru.Context.Set<SysTableConfig>()
                               group a by a.TableName into g
                               select g.Key;
                listHas = queryHas.ToList();

                var dbname = ru.Context.Database.GetDbConnection().Database;
                var dt = SQLServerDB.GetDataTable($"SELECT name FROM " + dbname + "..SysObjects Where XType = 'U' ORDER BY Name");
                listAll = dt.Select().Select(x => x[0].ToString()).ToList();
            }

            return new
            {
                listAll,
                listHas
            }.ToJson();
        }

        public string SQLServerTableInfoBuilder(string names, int cover)
        {
            var ListTableName = names.Split(',').ToList();

            if (cover == 1)
            {
                using (var ru = new RepositoryUse())
                {
                    ru.SysTableConfigRepository.Delete(x => ListTableName.Contains(x.TableName));
                }
            }

            string sql = string.Empty;
            string path = GlobalVar.WebRootPath + "/script/GetTableInfo.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                sql = sr.ReadToEnd();
            }

            foreach (var item in ListTableName)
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TableName", SqlDbType.VarChar, 50)
                };
                param[0].Value = item.ToLower();
                var dt = SQLServerDB.GetDataTable(sql, param);
                SQLServerDB.SqlBulkCopyByDataTable("SysTableConfig", dt);
            }

            return "success";
        }
        #endregion

        #region SQLServer 重置数据库初始化脚本
        public IActionResult SQLServerReset()
        {
            return Content("暂时取消，正在计划新的方案");
            //string result = string.Empty;
            //var nowdt = DateTime.Now;
            //string cmd = "Sqlcmd ";
            //using (var ru = new RepositoryUse())
            //{
            //    var dbconn = ru.Context.Database.GetDbConnection();
            //    var listConn = dbconn.ConnectionString.Split(';').ToList();
            //    string uid = string.Empty;
            //    string pwd = string.Empty;
            //    foreach (var kv in listConn)
            //    {
            //        var listkv = kv.Split('=').ToList();
            //        string key = listkv.FirstOrDefault().ToLower();

            //        if ("uid,user id".Split(',').ToList().Contains(key))
            //        {
            //            uid = listkv.LastOrDefault();
            //        }
            //        if ("pwd,password".Split(',').ToList().Contains(key))
            //        {
            //            pwd = listkv.LastOrDefault();
            //        }
            //    }

            //    cmd += "-S " + dbconn.DataSource + " -U " + uid + " -P " + pwd + " -d " + dbconn.Database;
            //}

            //cmd += " -i " + GlobalVar.WebRootPath + "/script/";

            //result += "--------- Delete all the tables" + Environment.NewLine;

            //var cmdout = Core.CmdTo.Run(cmd + "DataDropTable.txt");

            //result += cmdout.Replace("Sqlcmd ", "^").Split('^').ToList().LastOrDefault();
            //result += Environment.NewLine;

            //result += "--------- Init tables" + Environment.NewLine;

            //cmdout = Core.CmdTo.Run(cmd + "DataInit.txt");
            //result += cmdout.Replace("Sqlcmd ", "^").Split('^').ToList().LastOrDefault();

            //result = result.Insert(0, "time consuming ：" + (DateTime.Now - nowdt).Seconds + " S" + Environment.NewLine);

            //return Content(result);
        }
        #endregion
    }
}