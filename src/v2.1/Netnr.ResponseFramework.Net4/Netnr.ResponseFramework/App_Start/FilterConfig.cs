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

                string msg = $"==========日志记录时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}{Environment.NewLine}"
                       + $"消息内容：{filterContext.Exception.Message}{Environment.NewLine}"
                       + $"引发异常的方法：{filterContext.Exception.StackTrace.Replace(Environment.NewLine, "^").Split('^')[0]}"
                       + Environment.NewLine + Environment.NewLine;
                try
                {
                    var date = DateTime.Now;
                    string path = "/logs/" + date.ToString("yyyyMM") + "/";
                    path = HttpContext.Current.Server.MapPath(path);
                    Core.FileTo.WriteText(msg, path, date.ToString("yyyyMMdd") + ".log");
                }
                catch (Exception) { }
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
                                dic.Add(action, remark);
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
                            Id = Guid.NewGuid().ToString(),
                            UserName = userinfo.UserName,
                            Action = controller + "/" + action,
                            Url = url,
                            Ip = ct.IPv4,
                            CreateTime = DateTime.Now,
                            BrowserName = ct.BrowserName,
                            SystemName = ct.SystemName,
                            LogGroup = 1
                        };
                        mo.LogContent = DicDescription[mo.Action.ToLower()];

                        System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                        {
                            using (var db = new ContextBase())
                            {
                                db.SysLog.Add(mo);
                                db.SaveChanges();
                            }
                        });
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
