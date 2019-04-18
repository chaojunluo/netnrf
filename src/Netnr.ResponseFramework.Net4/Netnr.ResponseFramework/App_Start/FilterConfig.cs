using Netnr.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netnr.ResponseFramework
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

                Core.ConsoleTo.Log(filterContext.Exception);
            }
        }

        private static Dictionary<string, string> _dicDescription;

        public static Dictionary<string, string> DicDescription
        {
            get
            {
                if (_dicDescription == null)
                {
                    var ass = System.Reflection.Assembly.GetExecutingAssembly();
                    var listController = ass.ExportedTypes.Where(x => x.BaseType.FullName == "System.Web.Mvc.Controller").ToList();

                    var dic = new Dictionary<string, string>();
                    foreach (var conll in listController)
                    {
                        var methods = conll.GetMethods();
                        foreach (var item in methods)
                        {
                            if (item.DeclaringType == conll)
                            {
                                string remark = "未备注说明";

                                var desc = item.CustomAttributes.Where(x => x.AttributeType == typeof(DescriptionAttribute)).FirstOrDefault();
                                if (desc != null)
                                {
                                    remark = desc.ConstructorArguments.FirstOrDefault().Value.ToString();
                                }
                                var action = (conll.Name.Replace("Controller", "/") + item.Name).ToLower();
                                if (!dic.ContainsKey(action))
                                {
                                    dic.Add(action, remark);
                                }
                            }
                        }
                    }
                    _dicDescription = dic;
                }

                return _dicDescription;
            }
            set
            {
                _dicDescription = value;
            }
        }

        /// <summary>
        /// 全局控制器 Ajax请求日志记录
        /// </summary>
        public class LogAjaxAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                var hc = filterContext.HttpContext;

                //是Ajax请求
                if (hc.Request.IsAjaxRequest())
                {
                    string controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
                    string action = filterContext.RouteData.Values["action"].ToString().ToLower();
                    string url = hc.Request.Path.ToString() + hc.Request.QueryString.ToString();

                    try
                    {
                        //客户端信息
                        var ct = new Core.ClientTo();

                        //用户信息
                        var userinfo = Func.Common.GetLoginUserInfo();

                        //日志保存
                        var mo = new Domain.SysLog()
                        {
                            LogId = Guid.NewGuid().ToString(),
                            SuName = userinfo.UserName,
                            LogAction = controller + "/" + action,
                            LogUrl = url,
                            LogIp = ct.IPv4,
                            LogCreateTime = DateTime.Now,
                            LogBrowserName = ct.BrowserName,
                            LogSystemName = ct.SystemName,
                            LogGroup = 1
                        };
                        mo.LogContent = DicDescription[mo.LogAction.ToLower()];

                        #region 分批写入日志

                        //分批写入满足的条件：缓存的日志数量
                        int cacheLogCount = 2000;
                        //分批写入满足的条件：缓存的时长，单位秒
                        int cacheLogTime = 60;

                        //日志记录
                        var cacheLogsKey = "Global_Logs";
                        //上次写入的时间
                        var cacheLogWriteKey = "Global_Logs_Write";

                        if (!(Core.CacheTo.Get(cacheLogsKey) is List<Domain.SysLog> cacheLogs))
                        {
                            cacheLogs = new List<Domain.SysLog>();
                        }
                        cacheLogs.Add(mo);

                        var cacheLogWrite = Core.CacheTo.Get(cacheLogWriteKey) as DateTime?;
                        if (!cacheLogWrite.HasValue)
                        {
                            cacheLogWrite = DateTime.Now;
                        }

                        if (cacheLogs?.Count > cacheLogCount || DateTime.Now.ToTimestamp() - cacheLogWrite.Value.ToTimestamp() > cacheLogTime)
                        {
                            using (var db = new ContextBase())
                            {
                                db.SysLog.AddRange(cacheLogs);
                                db.SaveChanges();
                            }

                            cacheLogs = null;
                            cacheLogWrite = DateTime.Now;
                        }

                        Core.CacheTo.Set(cacheLogsKey, cacheLogs, 3600 * 24 * 30);
                        Core.CacheTo.Set(cacheLogWriteKey, cacheLogWrite, 3600 * 24 * 30);

                        #endregion
                    }
                    catch (Exception)
                    {
                        //throw new System.Exception("写入操作日志失败");
                    }
                }

                base.OnActionExecuted(filterContext);
            }
        }
    }
}
