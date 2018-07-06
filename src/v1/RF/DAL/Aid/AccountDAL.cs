using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace RF.DAL
{
    public class AccountDAL
    {
        #region 登录

        /// <summary>
        /// 登录授权 结果
        /// </summary>
        private class LoginAuthModel
        {
            private int _code;
            /// <summary>
            /// 编码
            /// </summary>
            public int code
            {
                get { return _code; }
                set { _code = value; }
            }

            private string _msg;
            /// <summary>
            /// 描述
            /// </summary>
            public string msg
            {
                get { return _msg; }
                set { _msg = value; }
            }

            private string _url;
            /// <summary>
            /// 跳转链接
            /// </summary>
            public string url
            {
                get { return _url; }
                set { _url = value; }
            }
        }

        /// <summary>
        /// 登录授权
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="captcha">验证码</param>
        /// <param name="remember">是否记住密码</param>
        /// <returns></returns>
        public static string LoginAuth(string username, string password, string captcha, bool remember = false)
        {
            LoginAuthModel mo = new LoginAuthModel();

            try
            {
                if (System.Web.HttpContext.Current.Session["captcha"] == null || System.Web.HttpContext.Current.Session["captcha"].ToString().ToLower() != captcha.ToLower())
                {
                    mo.code = 102;
                    mo.msg = "验证码过期或错误";
                }
                else
                {
                    string sql = "select * from view_sys_user where u_name=@u_name and u_pwd=@u_pwd";
                    SQLiteParameter[] parma = {
                                                  new SQLiteParameter("@u_name",DbType.String,40),
                                                  new SQLiteParameter("@u_pwd",DbType.String,40)
                                              };
                    parma[0].Value = username;
                    parma[1].Value = DB.CalcTo.MD5(password);

                    DataTable dt = DB.HelperSQLite.Query(sql, parma).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        mo.code = 103;
                        mo.msg = "账号或密码错误";
                    }
                    else
                    {
                        DataRow dr = dt.Rows[0];

                        if (dr["u_state"].ToString() != "1")
                        {
                            mo.code = 104;
                            mo.msg = "账号已被禁用";
                        }
                        else
                        {

                            HttpCookie hc1 = new HttpCookie("__U_id");
                            HttpCookie hc2 = new HttpCookie("__U_name");
                            HttpCookie hc3 = new HttpCookie("__U_nickname");
                            HttpCookie hc4 = new HttpCookie("__U_roleid");
                            HttpCookie hc5 = new HttpCookie("__U_photo");

                            hc1.Value = DB.CalcTo.EnHash(dr["id"].ToString());
                            hc2.Value = DB.CalcTo.EnHash(dr["u_name"].ToString());
                            hc3.Value = DB.CalcTo.EnHash(dr["u_nickname"].ToString());
                            hc4.Value = DB.CalcTo.EnHash(dr["u_roleid"].ToString());
                            hc5.Value = DB.CalcTo.EnHash(dr["u_photo"].ToString());

                            if (remember)
                            {
                                hc1.Expires = hc2.Expires = hc3.Expires = hc4.Expires = hc5.Expires = DateTime.Now.AddDays(5);
                            }

                            System.Web.HttpContext.Current.Response.Cookies.Add(hc1);
                            System.Web.HttpContext.Current.Response.Cookies.Add(hc2);
                            System.Web.HttpContext.Current.Response.Cookies.Add(hc3);
                            System.Web.HttpContext.Current.Response.Cookies.Add(hc4);
                            System.Web.HttpContext.Current.Response.Cookies.Add(hc5);

                            mo.code = 100;
                            mo.msg = "登录成功";
                            mo.url = "/";

                            //角色权限菜单、按钮缓存
                            DB.CatchTo.Set("role" + dr["u_roleid"].ToString(), dr);

                            //登录票据
                            System.Web.Security.FormsAuthentication.SetAuthCookie(dr["id"].ToString(), remember);
                        }
                    }
                }
            }
            catch (Exception)
            {
                mo.code = 101;
                mo.msg = "处理登录授权异常";
            }

            return mo.ToJson();
        }


        #endregion

        #region 获取 Cookie

        /// <summary>
        /// 获取登录用户id
        /// </summary>
        /// <returns></returns>
        public static string U_id()
        {
            string result = string.Empty;
            HttpCookie hc = HttpContext.Current.Request.Cookies["__U_id"];
            if (hc != null)
            {
                result = DB.CalcTo.DeHash(hc.Value);
            }
            return result;
        }

        /// <summary>
        /// 获取登录用户name
        /// </summary>
        /// <returns></returns>
        public static string U_name()
        {
            string result = string.Empty;
            HttpCookie hc = HttpContext.Current.Request.Cookies["__U_name"];
            if (hc != null)
            {
                result = DB.CalcTo.DeHash(hc.Value);
            }
            return result;
        }

        /// <summary>
        /// 获取登录用户nickname
        /// </summary>
        /// <returns></returns>
        public static string U_nickname()
        {
            string result = string.Empty;
            HttpCookie hc = HttpContext.Current.Request.Cookies["__U_nickname"];
            if (hc != null)
            {
                result = DB.CalcTo.DeHash(hc.Value);
            }
            return result;
        }

        /// <summary>
        /// 获取登录用户roleid
        /// </summary>
        /// <returns></returns>
        public static string U_roleid()
        {
            string result = string.Empty;
            HttpCookie hc = HttpContext.Current.Request.Cookies["__U_roleid"];
            if (hc != null)
            {
                result = DB.CalcTo.DeHash(hc.Value);
            }
            return result;
        }

        #endregion
    }
}