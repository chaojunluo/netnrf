using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netnr.Func
{
    /// <summary>
    /// 基于Cookie实现的Session会话
    /// </summary>
    public class Session
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public HttpContext Context { get; set; }

        /// <summary>
        /// Cookie名
        /// </summary>
        public string Name { get; set; } = "__Session";

        /// <summary>
        /// 过期时间，默认5分钟
        /// </summary>
        public DateTime TimeOut { get; set; } = DateTime.Now.AddMinutes(5);

        /// <summary>
        /// 加密因子
        /// </summary>
        private string SecretKey { get; set; } = "netnrf";

        public Session(HttpContext context, string CookieName = null, DateTime? CookieTimeOut = null)
        {
            Context = context;
            if (!string.IsNullOrWhiteSpace(CookieName))
            {
                Name = CookieName;
            }
            if (CookieTimeOut.HasValue)
            {
                TimeOut = CookieTimeOut.Value;
            }
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool TryGetValue(string Key, out string Value)
        {
            var b = true;
            string val = null;
            try
            {
                if (Context.Request.Cookies.TryGetValue(Name, out string cooks))
                {
                    var jo = JObject.Parse(Core.CalcTo.DeDES(cooks, SecretKey));
                    if (jo.TryGetValue(Key, out JToken jval))
                    {
                        val = jval.ToString();
                    }
                }
            }
            catch (Exception)
            {
                b = false;
            }
            Value = val;
            return b;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Set(string Key, string Value)
        {
            var jo = new JObject();
            if (Context.Request.Cookies.TryGetValue(Name, out string cooks))
            {
                jo = JObject.Parse(Core.CalcTo.DeDES(cooks, SecretKey));
            }
            jo[Key] = Value;
            string newcooks = Core.CalcTo.EnDES(jo.ToJson(), SecretKey);
            Context.Response.Cookies.Append(Name, newcooks, new CookieOptions()
            {
                HttpOnly = true,
                Expires = TimeOut,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
