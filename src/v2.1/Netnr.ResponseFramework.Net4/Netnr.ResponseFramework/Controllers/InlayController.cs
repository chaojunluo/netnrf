using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using Newtonsoft.Json.Linq;

namespace Netnr.ResponseFramework.Controllers
{
    public class InlayController : Controller
    {
        #region 局部视图
        [Description("按钮局部视图")]
        [AllowAnonymous]
        public ActionResult Button(string current_url)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new EmptyResult();
            }

            //按钮列表
            var listBtn = new List<SysButton>();

            //根据路由反查页面对应的菜单
            var moMenu = Func.Common.QuerySysMenuList(x => x.Url == current_url).FirstOrDefault();
            if (moMenu != null)
            {
                //登录用户的角色信息
                var luri = Func.Common.LoginUserRoleInfo();
                if (luri != null && !string.IsNullOrWhiteSpace(luri.Buttons))
                {
                    //角色配置的按钮
                    var joRole = JObject.Parse(luri.Buttons);
                    //根据菜单ID取对应的按钮
                    string btns = joRole[moMenu.Id].ToStringOrEmpty();
                    if (!string.IsNullOrWhiteSpace(btns))
                    {
                        var btnids = btns.Split(',').ToList();

                        //根据按钮ID取按钮
                        listBtn = Func.Common.QuerySysButtonList(x => btnids.Contains(x.Id)).ToList();
                    }
                }
            }

            return View(listBtn);
        }

        /// <summary>
        /// 模态框表单组件
        /// </summary>
        /// <param name="tablename">列配置表名</param>
        /// <param name="paneltitle">面板标题（有两个区域或以上时，英文逗号分隔）</param>
        /// <param name="modalsize">模态框大小 1小 2中 3大</param>
        /// <param name="index">表单ID后缀</param>
        /// <returns></returns>
        [Description("表单局部视图")]
        public ActionResult Form(string tablename, string paneltitle = "", int modalsize = 3, int index = 1)
        {
            using (var ru = new ContextBase())
            {
                //查询表对应的表单，排序，按区域分组
                var listCol = ru.SysTableConfig.Where(x => x.TableName == tablename).OrderBy(x => x.FormOrder).ToList();

                var groupCol = listCol.GroupBy(x => x.FormArea.Value).ToList();

                //表名
                TempData["tablename"] = tablename;
                //ID索引
                TempData["index"] = index;
                //模态框大小
                TempData["modalsize"] = modalsize == 3 ? "modal-lg" : modalsize == 1 ? "modal-sm" : "";
                //多面板标题
                TempData["paneltitle"] = paneltitle.Split(',').ToList();

                return View(groupCol);
            }
        }
        #endregion

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
                    string id = jt["Id"].ToString();

                    var mo = listRow.Where(x => x.Id == id).FirstOrDefault();

                    mo.ColTitle = jt["ColTitle"].ToStringOrEmpty();
                    mo.ColAlign = string.IsNullOrWhiteSpace(jt["ColAlign"].ToStringOrEmpty()) ? 1 : Convert.ToInt32(jt["ColAlign"].ToStringOrEmpty());
                    mo.ColWidth = string.IsNullOrWhiteSpace(jt["ColWidth"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColWidth"].ToStringOrEmpty());
                    mo.ColHide = string.IsNullOrWhiteSpace(jt["ColHide"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColHide"].ToStringOrEmpty());
                    mo.ColFrozen = string.IsNullOrWhiteSpace(jt["ColFrozen"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColFrozen"].ToStringOrEmpty());
                    mo.ColExport = string.IsNullOrWhiteSpace(jt["ColExport"].ToStringOrEmpty()) ? 0 : Convert.ToInt32(jt["ColExport"].ToStringOrEmpty());
                    mo.ColOrder = order++;
                }

                db.SysTableConfig.UpdateRange(listRow, db);
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

                db.SysTableConfig.UpdateRange(listRow, db);
                int num = db.SaveChanges();

                return num >= 0 ? "success" : "fail";
            }
        }
        #endregion
    }
}