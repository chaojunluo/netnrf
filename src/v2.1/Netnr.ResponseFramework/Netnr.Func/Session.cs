using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;

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
        public string Name { get; set; } = ".session";

        /// <summary>
        /// 加密因子
        /// </summary>
        private string SecretKey { get; set; } = "netnr";

        /// <summary>
        /// 过期
        /// </summary>
        private string ExpireSuffix { get; set; } = "-Expires";

        public Session(HttpContext context, string CookieName = null)
        {
            Context = context;
            if (!string.IsNullOrWhiteSpace(CookieName))
            {
                Name = CookieName;
            }
        }

        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="context"></param>
        private void SaveCookie(string context)
        {
            Context.Response.Cookies.Append(Name, context, new CookieOptions()
            {
                HttpOnly = true
            });
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
                    if (DateTime.Parse(jo[Key + ExpireSuffix].ToString()) > DateTime.Now)
                    {
                        val = jo[Key].ToString();
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
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <param name="Expiress">过期时间 默认5分钟</param>
        public void Set(string Key, string Value, DateTime? Expiress = null)
        {
            var jo = new JObject();
            if (Context.Request.Cookies.TryGetValue(Name, out string cooks))
            {
                jo = JObject.Parse(Core.CalcTo.DeDES(cooks, SecretKey));
            }
            jo[Key] = Value;
            jo[Key + ExpireSuffix] = Expiress ?? DateTime.Now.AddMinutes(5);
            string newcooks = Core.CalcTo.EnDES(jo.ToJson(), SecretKey);
            SaveCookie(newcooks);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Key">键</param>
        public void Remove(string Key)
        {
            var jo = new JObject();
            if (Context.Request.Cookies.TryGetValue(Name, out string cooks))
            {
                jo = JObject.Parse(Core.CalcTo.DeDES(cooks, SecretKey));
            }
            jo.Remove(Key);
            jo.Remove(Key + ExpireSuffix);
            string newcooks = Core.CalcTo.EnDES(jo.ToJson(), SecretKey);
            SaveCookie(newcooks);
        }
    }
}
