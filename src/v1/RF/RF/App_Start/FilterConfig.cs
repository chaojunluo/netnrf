using System.Web;
using System.Web.Mvc;

namespace RF
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new LogAjaxAttribute());
        }

        /// <summary>
        /// 全局控制器异常记录
        /// </summary>
        public class CustomErrorAttribute : HandleErrorAttribute
        {
            public override void OnException(ExceptionContext filterContext)
            {
                base.OnException(filterContext);

                string ErrorMsg = string.Format("==========日志记录时间：{0}\r\n消息内容：{1}\r\n引发异常的方法：{2}\r\n引发异常源：{3}\r\n",
                    System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), filterContext.Exception.Message, filterContext.Exception.TargetSite,
                    filterContext.Exception.StackTrace.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries)[0]);
                try
                {
                    string path = "/_Log/";
                    string date = System.DateTime.Now.ToString("yyyyMMdd");
                    path += date.Substring(0, 6) + "/";
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                    string filePath = HttpContext.Current.Server.MapPath(path + date + ".log");
                    System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.File.Exists(filePath) ?
                        System.IO.FileMode.Append : System.IO.FileMode.Create);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                    sw.WriteLine(ErrorMsg);
                    sw.Close();
                    fs.Close();
                }
                catch (System.Exception) { }
            }
        }

        /// <summary>
        /// 全局控制器 Ajax请求日志记录
        /// </summary>
        public class LogAjaxAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                HttpContextBase hc = filterContext.HttpContext;

                //是Ajax请求
                if (hc.Request.IsAjaxRequest())
                {
                    string controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
                    string action = filterContext.RouteData.Values["action"].ToString().ToLower();

                    //日志
                    RF.Model.sys_log ml = new Model.sys_log();
                    ml.l_action = controller + "/" + action;
                    ml.l_datetime = System.DateTime.Now;
                    ml.l_url = hc.Request.Url.ToString();
                    ml.l_user = DAL.AccountDAL.U_name();
                    ml.l_spare = DAL.AccountDAL.U_nickname();
                    ml.l_ip = DB.ClienTo.IP();
                    ml.l_content = "其它操作";

                    #region 填充操作说明
                    switch (controller)
                    {
                        case "account":
                            #region Account
                            switch (action)
                            {
                                case "loginauth":
                                    ml.l_content = "用户登录";
                                    break;
                                case "logout":
                                    ml.l_content = "用户注销";
                                    break;
                            }
                            #endregion
                            break;

                        case "common":
                            #region Common
                            switch (action)
                            {
                                case "querydata":
                                    string aps = string.Empty;
                                    switch (hc.Request["uri"])
                                    {
                                        case "pptableconfig":
                                            aps = "表格配置";
                                            break;
                                        case "pprole":
                                            aps = "系统角色";
                                            break;
                                        case "pplog":
                                            aps = "操作日志";
                                            break;
                                        case "ppcolumn":
                                            aps = "列表配置";
                                            break;
                                        case "ppuser":
                                            aps = "系统用户";
                                            break;

                                    }
                                    ml.l_content = "查询" + aps;
                                    break;
                                case "querysysrolebox":
                                    ml.l_content = "查询系统角色列表";
                                    break;
                            }
                            #endregion
                            break;

                        case "partial":
                            #region Partial
                            switch (action)
                            {
                                case "savetableconfig":
                                    ml.l_content = "保存表格配置（" + hc.Request["vname"].ToString() + ")";
                                    break;
                                case "saveformconfig":
                                    ml.l_content = "保存表单配置（" + hc.Request["vname"].ToString() + ")";
                                    break;
                            }
                            #endregion
                            break;

                        case "setting":
                            #region Setting
                            switch (action)
                            {
                                case "savesysrole":
                                    ml.l_content = "保存系统角色";
                                    break;
                                case "savesysroleauth":
                                    ml.l_content = "保存系统角色权限配置";
                                    break;
                                case "delsysrole":
                                    ml.l_content = "删除系统角色";
                                    break;
                                case "savesyscolumn":
                                    ml.l_content = "保存列表配置";
                                    break;
                                case "delsyscolumn":
                                    ml.l_content = "删除列表配置";
                                    break;
                            }
                            #endregion
                            break;
                    }
                    #endregion

                    //保存
                    try
                    {
                        new DAL.sys_log().Add(ml);
                    }
                    catch (System.Exception)
                    {
                        //throw new System.Exception("写入操作日志失败");
                    }
                }

                base.OnActionExecuted(filterContext);
            }
        }
    }
}