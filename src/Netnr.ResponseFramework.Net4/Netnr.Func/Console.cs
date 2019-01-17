using System;

namespace Netnr.Func
{
    /// <summary>
    /// 输出
    /// </summary>
    public class Console
    {
        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="ex"></param>
        public static void Log(Exception ex)
        {
            string msg = $"==========日志记录时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}{Environment.NewLine}"
                       + $"消息内容：{ex.Message}{Environment.NewLine}"
                       + $"引发异常的方法：{ex.StackTrace.Replace(Environment.NewLine, "^").Split('^')[0]}"
                       + Environment.NewLine + Environment.NewLine;
            Log(msg);
        }

        /// <summary>
        /// 写入消息
        /// </summary>
        /// <param name="msg"></param>
        public static void Log(string msg)
        {
            DateTime dt = DateTime.Now;
            var path = "/logs/" + dt.ToString("yyyyMM") + "/";
            path = System.Web.HttpContext.Current.Server.MapPath(path);
            Core.FileTo.WriteText(msg, path, "console_" + dt.ToString("yyyyMMdd") + ".log");
        }
    }
}
