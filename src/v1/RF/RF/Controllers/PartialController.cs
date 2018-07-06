using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RF.Controllers
{
    /// <summary>
    /// 分部视图
    /// </summary>
    [AllowAnonymous]
    public class PartialController : Controller
    {
        #region 操作按钮权控

        public ActionResult Action_BuildButton()
        {
            try
            {
                //获取 菜单、按钮、角色权限 
                DataTable dt_menu = DAL.PartialDAL.GetSysMenu();
                DataTable dt_button = DAL.PartialDAL.GetSysButton();
                DataRow dr_role = DAL.PartialDAL.GetSysRole();

                //根据角色权限 生成 菜单页面按钮
                string current_url = Request.Url.AbsolutePath;
                DataRow[] dr_menu = dt_menu.Select("m_url='" + current_url.OfSql() + "'");
                if (dr_menu.Count() == 0)
                {
                    return new EmptyResult();
                }
                else
                {
                    try
                    {
                        //角色权限
                        JObject jo_role = JObject.Parse(dr_role["r_buttons"].ToString());
                        //根据菜单id取对应的按钮id
                        string btns = jo_role[dr_menu[0]["id"].ToString()].ToTokenString();
                        if (btns != "")
                        {
                            DataRow[] dr_button = dt_button.Select("id in(" + btns + ")");
                            List<Model.sys_button> listBtn = new List<Model.sys_button>();
                            foreach (DataRow dr in dr_button)
                            {
                                listBtn.Add(new DAL.sys_button().DataRowToModel(dr));
                            }
                            return PartialView(listBtn);
                        }
                        return new EmptyResult();
                    }
                    catch (Exception)
                    {
                        return new EmptyResult();
                    }
                }
            }
            catch (Exception)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                return new EmptyResult();
            }
        }

        #endregion

        #region 表单生成

        /// <summary>
        /// 表单生成
        /// </summary>
        /// <param name="vname">虚拟表名</param>
        /// <param name="modalsize">模态大小，1：小  2：大 默认2</param>
        /// <returns></returns>
        public ActionResult Action_BuildForm(string vname, string modalsize = "2")
        {
            try
            {
                //虚拟表名
                vname = vname.OfSql();
                DataTable dtC = DB.HelperSQLite.Query("select * from sys_column where vname='" + vname + "' order by f_area,f_order").Tables[0];

                //分区域的 集合
                Dictionary<int, List<RF.Model.sys_column>> dic = new Dictionary<int, List<Model.sys_column>>();

                //按区域分组
                IEnumerable<IGrouping<int, DataRow>> igs = dtC.Rows.Cast<DataRow>().GroupBy<DataRow, int>(d => d["f_area"].Equals(DBNull.Value) ? 1 : Convert.ToInt32(d["f_area"]));
                foreach (IGrouping<int, DataRow> ig in igs)
                {
                    List<RF.Model.sys_column> listC = new List<Model.sys_column>();
                    foreach (DataRow dr in ig)
                    {
                        listC.Add(new DAL.sys_column().DataRowToModel(dr));
                    }
                    dic.Add(Convert.ToInt32(ig.First()["f_area"].Equals(DBNull.Value) ? 1 : ig.First()["f_area"]), listC);
                }

                //ID索引
                int index = string.IsNullOrEmpty(Request["index"]) ? 1 : Convert.ToInt32(Request["index"]);
                ViewBag.index = index;
                ViewBag.modalsize = modalsize == "2" ? "modal-lg" : "";
                return PartialView(dic);
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
        }
        #endregion

        #region 配置表格

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <returns></returns>
        public string SaveTableConfig()
        {
            string vname = Request["vname"].ToString();
            string rows = Request["rows"].ToString();
            JArray ja = JArray.Parse(rows);
            StringBuilder sb = new StringBuilder();
            int order = 0;
            foreach (JToken jt in ja)
            {
                string title = jt["title"].ToTokenString();
                int id, width, align, hide, frozen;
                id = Convert.ToInt32(jt["id"].ToTokenString());
                if (!int.TryParse(jt["width"].ToTokenString(), out width))
                {
                    width = 0;
                }
                if (!int.TryParse(jt["align"].ToTokenString(), out align))
                {
                    align = 1;
                }
                if (!int.TryParse(jt["hide"].ToTokenString(), out hide))
                {
                    hide = 0;
                }
                if (!int.TryParse(jt["frozen"].ToTokenString(), out frozen))
                {
                    frozen = 0;
                }
                order++;
                sb.AppendFormat("update sys_column set title='{0}',width={1},align={2},hide={3},frozen={4},l_order={5} where id={6} and vname='{7}';", title.OfSql(), width, align, hide, frozen, order, id, vname.OfSql());
            }
            int n = DB.HelperSQLite.ExecuteSql(sb.ToString());
            return n >= 0 ? "success" : "fail";
        }

        #endregion

        #region 配置表单
        public string SaveFormConfig()
        {
            string vname = Request["vname"].ToString().OfSql();
            string rows = Request["rows"].ToString();
            JArray ja = JArray.Parse(rows);
            StringBuilder sb = new StringBuilder();
            int order = 1;
            foreach (JToken jt in ja)
            {
                string title = jt["title"].ToTokenString().OfSql();
                string field = jt["field"].ToTokenString().OfSql();
                int col, area;
                if (!int.TryParse(jt["col"].ToTokenString(), out col))
                {
                    col = 1;
                }
                if (!int.TryParse(jt["area"].ToTokenString(), out area))
                {
                    area = 1;
                }
                sb.AppendFormat("update sys_column set title='{0}',f_col={1},f_area={2},f_order={3} where field='{4}' and vname='{5}';", title, col, area, order, field, vname);
                order++;
            }
            int n = DB.HelperSQLite.ExecuteSql(sb.ToString());
            return n >= 0 ? "success" : "fail";
        }
        #endregion

        #region 数据库重置

        /// <summary>
        /// 重置站点数据库
        /// </summary>
        /// <returns></returns>
        public string SiteReset()
        {
            string result = string.Empty;
            string app_data = Server.MapPath("/App_Data/");
            try
            {
                System.IO.File.Copy(app_data + "_sqlite.db", app_data + "sqlite.db", true);
                result = "success";
            }
            catch (Exception)
            {
                result = "fail";
            }
            return result;
        }

        #endregion
    }
}
