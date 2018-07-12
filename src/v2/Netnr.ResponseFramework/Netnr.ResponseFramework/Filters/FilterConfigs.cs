using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
                DateTime dt = DateTime.Now;

                string path = Startup.HostingEnvironment.ContentRootPath + "\\logs\\" + dt.ToString("yyyyMM") + "\\";

                string msg = $"==========日志记录时间：{dt.ToString("yyyy-MM-dd HH:mm:ss")}{Environment.NewLine}"
                           + $"消息内容：{context.Exception.Message}{Environment.NewLine}"
                           + $"引发异常的方法：{context.Exception.StackTrace.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)[0]}"
                           + Environment.NewLine + Environment.NewLine;

                Core.FileTo.WriteText(msg, path, dt.ToString("yyyyMMdd") + ".log");
            }
        }

        /// <summary>
        /// 全局日志记录
        /// </summary>
        public class LogActionAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuted(ResultExecutedContext context)
            {
                //异步获取系统信息于缓存
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    var hc = context.HttpContext;

                    string controller = context.RouteData.Values["controller"].ToString().ToLower();
                    string action = context.RouteData.Values["action"].ToString().ToLower();
                    string url = hc.Request.Path.ToString() + hc.Request.QueryString.Value;

                    //判断是Ajax
                    bool isAjax = false;
                    if (hc.Request.Headers.ContainsKey("x-requested-with"))
                    {
                        isAjax = hc.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                    }

                    if (!isAjax)
                    {
                        //不是Ajax也记录
                        string needlog = $"logout";
                        if (needlog.Contains(action))
                        {
                            isAjax = true;
                        }
                    }

                    if (isAjax)
                    {
                        try
                        {
                            //客户端信息
                            var ct = new Core.ClientTo(hc);

                            //用户信息
                            var userinfo = Func.Common.GetLoginUserInfo(hc);

                            //日志保存
                            var mo = new Domain.SysLog()
                            {
                                ID = Guid.NewGuid().ToString(),
                                UserName = userinfo.UserName,
                                Nickname = userinfo.Nickname,
                                Action = controller + "/" + action,
                                Url = url,
                                Ip = ct.IPv4,
                                CreateTime = DateTime.Now,
                                BrowserName = ct.BrowserName,
                                SystemName = ct.SystemName,
                                LogGroup = 1,

                                LogContent = Func.ProjectDocs.QueryNotes(controller, action, hc)
                            };

                            using (var ru = new Data.RepositoryUse())
                            {
                                ru.SysLogRepository.Insert(mo);
                            }
                        }
                        catch (Exception)
                        {
                            //throw new System.Exception("写入操作日志失败");
                        }
                    }
                });

                base.OnResultExecuted(context);
            }
        }
    }
}
