using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using Newtonsoft.Json.Linq;

namespace Netnr.ResponseFramework.Controllers
{
    [Authorize]
    public class InlayController : Controller
    {
        #region 配置表格

        [Description("查询配置表格")]
        public string QueryConfigTable(QueryDataVM.GetParams param)
        {
            var or = new QueryDataVM.OutputResult();

            using (var db = new ContextBase())
            {
                var query = db.SysTableConfig;
                Func.Common.QueryJoin(query, param, db, ref or);
            }
            return or.ToJson();
        }

        [Description("保存配置表格")]
        public string SaveConfigTable(string rows, string tablename)
        {
            JArray ja = JArray.Parse(rows);

            using (var db = new ContextBase())
            {
                var listRow = db.SysTableConfig.Where(x => x.TableName == tablename).ToList();

                int order = 0;
                foreach (JToken jt in ja)
                {
                    string id = jt["ID"].ToString();

                    var mo = listRow.Where(x => x.Id == id).FirstOrDefault();

                    mo.ColTitle = jt["ColTitle"].ToStringOrEmpty();
                    mo.ColAlign = string.IsNullOrWhiteSpace(jt["ColAlign"].ToStringOrEmpty()) ? 1 : Convert.ToInt32(jt["ColAlign"].ToStringOrEmpty());
                    mo.ColWidth = string.IsNullOrWhiteSpace(jt["ColWidth"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColWidth"].ToStringOrEmpty());
                    mo.ColHide = string.IsNullOrWhiteSpace(jt["ColHide"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColHide"].ToStringOrEmpty());
                    mo.ColFrozen = string.IsNullOrWhiteSpace(jt["ColFrozen"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColFrozen"].ToStringOrEmpty());
                    mo.ColExport = string.IsNullOrWhiteSpace(jt["ColExport"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColExport"].ToStringOrEmpty());
                    mo.ColOrder = order++;
                }

                db.SysTableConfig.UpdateRange(listRow);
                int num = db.SaveChanges();

                return num >= 0 ? "success" : "fail";
            }
        }

        #endregion

        #region 配置表单

        [Description("保存配置表单")]
        public string SaveConfigForm(string rows, string tablename)
        {
            JArray ja = JArray.Parse(rows);

            using (var db = new ContextBase())
            {
                var listRow = db.SysTableConfig.Where(x => x.TableName == tablename).ToList();

                int order = 0;
                foreach (JToken jt in ja)
                {
                    string field = jt["field"].ToStringOrEmpty();
                    var mo = listRow.Where(x => x.ColField == field).FirstOrDefault();

                    mo.ColTitle = jt["title"].ToStringOrEmpty();
                    mo.FormSpan = string.IsNullOrWhiteSpace(jt["span"].ToStringOrEmpty()) ? 1 : Convert.ToInt32(jt["span"].ToStringOrEmpty());
                    mo.FormArea = string.IsNullOrWhiteSpace(jt["area"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["area"].ToStringOrEmpty());
                    mo.FormOrder = order++;
                }

                db.SysTableConfig.UpdateRange(listRow);
                int num = db.SaveChanges();

                return num >= 0 ? "success" : "fail";
            }
        }
        #endregion
    }
}