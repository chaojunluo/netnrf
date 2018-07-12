using System;
using System.Collections.Generic;

namespace Netnr.Func
{
    public class ProjectDocs
    {
        /// <summary>
        /// 查询注释
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法</param>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public static string QueryNotes(string controller, string action, Microsoft.AspNetCore.Http.HttpContext context)
        {
            var notes = new List<string>();

            string url = context.Request.Path.ToString();

            #region 说明
            switch (controller)
            {
                case "account":
                    #region Account
                    switch (action)
                    {
                        case "login":
                            notes.Add("访问登录页面");
                            break;
                        case "loginvalidation":
                            notes.Add("用户登录");
                            break;
                        case "logout":
                            notes.Add("用户注销");
                            break;
                        case "updatepassword":
                            notes.Add("访问修改密码页面");
                            break;
                        case "updatenewpassword":
                            notes.Add("修改密码");
                            break;
                    }
                    #endregion
                    break;

                case "common":
                    #region Common
                    switch (action)
                    {
                        case "querymenu":
                            notes.Add("查询系统菜单");
                            break;
                        case "querydata":
                            string qd = string.Empty;
                            string uri = context.Request.Query["uri"];
                            switch (uri)
                            {
                                case "systableconfig":
                                    qd = "表配置";
                                    break;
                                case "sysrole":
                                    qd = "系统角色";
                                    break;
                                case "syslog":
                                    qd = "操作日志";
                                    break;
                                case "sysuser":
                                    qd = "系统用户";
                                    break;
                                default:
                                    qd = uri;
                                    break;
                            }
                            notes.Add("查询" + qd);
                            break;
                    }
                    #endregion
                    break;

                case "inlay":
                    #region Inlay
                    switch (action)
                    {
                        case "saveconfigtable":
                            notes.Add("保存表格配置（" + context.Request.Form["tablename"].ToString() + ")");
                            break;
                        case "saveconfigform":
                            notes.Add("保存表单配置（" + context.Request.Form["tablename"].ToString() + ")");
                            break;
                    }
                    #endregion
                    break;

                case "io":
                    #region IO
                    switch (action)
                    {
                        case "export":
                            notes.Add("导出Excel（" + context.Request.Form["uri"].ToString() + ")");
                            break;
                    }
                    #endregion
                    break;

                case "setting":
                    #region Setting
                    switch (action)
                    {
                        case "sysrole":
                            notes.Add("访问系统角色页面");
                            break;
                        case "savesysrole":
                            notes.Add("保存系统角色");
                            break;
                        case "delsysrole":
                            notes.Add("删除系统角色");
                            break;
                        case "sysuser":
                            notes.Add("访问系统用户页面");
                            break;
                        case "querysysuser":
                            notes.Add("查询系统用户");
                            break;
                        case "savesysuser":
                            notes.Add("保存系统用户");
                            break;
                        case "delsysuser":
                            notes.Add("删除系统用户");
                            break;
                        case "syslog":
                            notes.Add("访问系统日志页面");
                            break;
                        case "systableconfig":
                            notes.Add("访问表配置页面");
                            break;
                        case "savesystableconfig":
                            notes.Add("保存表配置");
                            break;
                        case "delsystableconfig":
                            notes.Add("删除表配置");
                            break;
                        case "sysstyle":
                            notes.Add("访问样式配置页面");
                            break;
                    }
                    #endregion
                    break;
            }
            #endregion

            return string.Join(",", notes);
        }
    }
}
