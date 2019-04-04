using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Netnr.ResponseFramework.Filters
{
    /// <summary>
    /// 过滤器
    /// 
    /// 能 实现一个过滤器接口，要么是同步版本的，要么是异步版本的，鱼和熊掌不可兼得 。
    /// 如果你需要在接口中执行异步工作，那么就去实现异步接口。否则应该实现同步版本的接口。
    /// 框架会首先检查是不是实现了异步接口，如果实现了异步接口，那么将调用它。
    /// 不然则调用同步接口的方法。如果一个类中实现了两个接口，那么只有异步方法会被调用。
    /// 最后，不管 action 是同步的还是异步的，过滤器的同步或是异步是独立于 action 的
    /// </summary>
    public class FilterConfigs
    {
        /// <summary>
        /// 全局错误处理
        /// </summary>
        public class ErrorActionFilter : IExceptionFilter
        {
            public void OnException(ExceptionContext context)
            {
                Core.ConsoleTo.Log(context.Exception);
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
                    var listController = ass.ExportedTypes.Where(x => x.BaseType.FullName == "Microsoft.AspNetCore.Mvc.Controller").ToList();

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
        /// 全局日志记录
        /// </summary>
        public class LogActionAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuted(ResultExecutedContext context)
            {
                var hc = context.HttpContext;

                string controller = context.RouteData.Values["controller"].ToString().ToLower();
                string action = context.RouteData.Values["action"].ToString().ToLower();
                string url = hc.Request.Path.ToString() + hc.Request.QueryString.Value;

                try
                {
                    //客户端信息
                    var ct = new Core.ClientTo(hc);

                    //用户信息
                    var userinfo = Func.Common.GetLoginUserInfo(hc);

                    //日志保存
                    var mo = new Domain.SysLog()
                    {
                        LogId = Guid.NewGuid().ToString(),
                        SuName = userinfo.UserName,
                        SuNickname = userinfo.Nickname,
                        LogAction = controller + "/" + action,
                        LogUrl = url,
                        LogIp = ct.IPv4,
                        LogCreateTime = DateTime.Now,
                        LogBrowserName = ct.BrowserName,
                        LogSystemName = ct.SystemName,
                        LogGroup = 1
                    };
                    mo.LogContent = DicDescription[mo.LogAction.ToLower()];

                    using (var db = new Data.ContextBase())
                    {
                        db.SysLog.Add(mo);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    //throw new System.Exception("写入操作日志失败");
                }

                base.OnResultExecuted(context);
            }
        }
    }
}