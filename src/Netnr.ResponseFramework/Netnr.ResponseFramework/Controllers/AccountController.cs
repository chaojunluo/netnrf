using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;
using Netnr.Login;

namespace Netnr.ResponseFramework.Controllers
{
    public class AccountController : Controller
    {
        #region 登录
        [Description("生成验证码")]
        public FileResult Captcha()
        {
            string num = Core.RandomTo.NumCode(4);
            byte[] bytes = Fast.ImageTo.CreateImg(num);
            Response.Cookies.Append("captcha", Core.CalcTo.MD5(num.ToLower()));
            return File(bytes, "image/jpeg");
        }

        [Description("登录页面")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="mo"></param>
        /// <param name="captcha">验证码</param>
        /// <param name="remember">1记住登录状态</param>
        /// <returns></returns>
        [Description("登录验证")]
        public async Task<AccountValidationVM> LoginValidation(SysUser mo, string captcha, int remember)
        {
            var result = new AccountValidationVM();

            var outMo = new SysUser();

            //跳过验证码
            if (captcha == "_pass_")
            {
                outMo = mo;
            }
            else
            {
                var capt = Request.Cookies["captcha"];

                if (string.IsNullOrWhiteSpace(captcha) || (capt ?? "") != Core.CalcTo.MD5(captcha.ToLower()))
                {
                    result.code = 104;
                    result.message = "验证码错误或已过期";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(mo.SuName) || string.IsNullOrWhiteSpace(mo.SuPwd))
                {
                    result.code = 101;
                    result.message = "用户名或密码不能为空";
                    return result;
                }

                using (var db = new ContextBase())
                {
                    outMo = db.SysUser.Where(x => x.SuName == mo.SuName && x.SuPwd == Core.CalcTo.MD5(mo.SuPwd, 32)).FirstOrDefault();
                }
            }

            if (outMo == null || string.IsNullOrWhiteSpace(outMo.SuId))
            {
                result.code = 102;
                result.message = "用户名或密码错误";
                return result;
            }

            if (outMo.SuStatus != 1)
            {
                result.code = 103;
                result.message = "用户已被禁止登录";
                return result;
            }

            try
            {
                #region 授权访问信息

                //登录信息
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, outMo.SuId));
                identity.AddClaim(new Claim(ClaimTypes.Name, outMo.SuName));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, outMo.SuNickname ?? ""));
                identity.AddClaim(new Claim(ClaimTypes.Role, outMo.SrId));
                //取值
                //HttpContext.User.FindFirstValue(ClaimTypes.Sid);

                //配置
                var authParam = new AuthenticationProperties();
                if (remember == 1)
                {
                    authParam.IsPersistent = true;
                    authParam.ExpiresUtc = DateTime.Now.AddDays(10);
                }

                //写入
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authParam);

                result.code = 100;
                result.message = "登录成功";
                result.url = "/";
                return result;

                #endregion
            }
            catch (Exception ex)
            {
                result.code = 105;
                result.message = "处理登录请求出错（" + ex.Message + "）";
                return result;
            }
        }
        #endregion

        #region 第三方
        /// <summary>
        /// 配置
        /// </summary>

        [Description("第三方登录授权跳转")]
        public IActionResult Auth()
        {
            string url = string.Empty;
            string vtype = RouteData.Values["id"]?.ToString().ToLower();
            switch (vtype)
            {
                case "qq":
                    url = QQ.AuthorizationHref(new QQ_Authorization_RequestEntity());
                    break;
                case "weibo":
                    url = Weibo.AuthorizeHref(new Weibo_Authorize_RequestEntity());
                    break;
                case "github":
                    url = GitHub.AuthorizeHref(new GitHub_Authorize_RequestEntity());
                    break;
                case "taobao":
                    url = Taobao.AuthorizeHref(new Taobao_Authorize_RequestEntity());
                    break;
                case "microsoft":
                    url = MicroSoft.AuthorizeHref(new MicroSoft_Authorize_RequestEntity());
                    break;
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "/account/login";
            }

            //已登录 && 从绑定页面点击
            if (HttpContext.User.Identity.IsAuthenticated && Request.Headers["Referer"].ToString().ToLower().Contains("authbind"))
            {
                //写入绑定标识cookie
                Response.Cookies.Append("AccountBindOAuth", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(2)
                });
            }
            else
            {
                //删除绑定标识
                Response.Cookies.Delete("AccountBindOAuth");
            }
            return Redirect(url);
        }

        [Description("授权登录回调")]
        public async Task<IActionResult> AuthCallback(string code)
        {
            var result = new AccountValidationVM();
            string vtype = RouteData.Values["id"]?.ToString().ToLower();
            try
            {
                //唯一标示
                string openId = string.Empty;
                try
                {
                    switch (vtype)
                    {
                        case "qq":
                            {
                                //获取 access_token
                                var accessToken_ResultEntity = QQ.AccessToken(new QQ_AccessToken_RequestEntity()
                                {
                                    code = code
                                });

                                //获取 OpendId
                                var openId_ResultEntity = QQ.OpenId(new QQ_OpenId_RequestEntity()
                                {
                                    access_token = accessToken_ResultEntity.access_token
                                });

                                //获取 UserInfo
                                var openId_Get_User_Info_ResultEntity = QQ.OpenId_Get_User_Info(new QQ_OpenAPI_RequestEntity()
                                {
                                    access_token = accessToken_ResultEntity.access_token,
                                    openid = openId_ResultEntity.openid
                                });

                                //身份唯一标识
                                openId = openId_ResultEntity.openid;
                            }
                            break;
                        case "weibo":
                            {
                                //获取 access_token
                                var accessToken_ResultEntity = Weibo.AccessToken(new Weibo_AccessToken_RequestEntity()
                                {
                                    code = code
                                });

                                //获取 access_token 的授权信息
                                var tokenInfo_ResultEntity = Weibo.GetTokenInfo(new Weibo_GetTokenInfo_RequestEntity()
                                {
                                    access_token = accessToken_ResultEntity.access_token
                                });

                                //获取 users/show
                                var userShow_ResultEntity = Weibo.UserShow(new Weibo_UserShow_RequestEntity()
                                {
                                    access_token = accessToken_ResultEntity.access_token,
                                    uid = Convert.ToInt64(tokenInfo_ResultEntity.uid)
                                });

                                openId = accessToken_ResultEntity.access_token;
                            }
                            break;
                        case "github":
                            {
                                //获取 access_token
                                var accessToken_ResultEntity = GitHub.AccessToken(new GitHub_AccessToken_RequestEntity()
                                {
                                    code = code
                                });

                                //获取 user
                                var user_ResultEntity = GitHub.User(new GitHub_User_RequestEntity()
                                {
                                    access_token = accessToken_ResultEntity.access_token
                                });

                                openId = user_ResultEntity.id.ToString();
                            }
                            break;
                        case "taobao":
                            {
                                //获取 access_token
                                var accessToken_ResultEntity = Taobao.AccessToken(new Taobao_AccessToken_RequestEntity()
                                {
                                    code = code
                                });

                                openId = accessToken_ResultEntity.open_uid;
                            }
                            break;
                        case "microsoft":
                            {
                                //获取 access_token
                                var accessToken_ResultEntity = MicroSoft.AccessToken(new MicroSoft_AccessToken_RequestEntity()
                                {
                                    code = code
                                });

                                //获取 user
                                var user_ResultEntity = MicroSoft.User(new MicroSoft_User_RequestEntity()
                                {
                                    access_token = accessToken_ResultEntity.access_token
                                });

                                openId = user_ResultEntity.id.ToString();
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    result.message = ex.Message;
                }

                if (string.IsNullOrWhiteSpace(openId))
                {
                    result.message = "身份验证失败";
                }
                else
                {
                    //判断是绑定操作
                    bool isbind = false;
                    if (User.Identity.IsAuthenticated)
                    {
                        var aboa = Request.Cookies["AccountBindOAuth"];
                        if (!string.IsNullOrWhiteSpace(aboa) && (DateTime.Now - DateTime.Parse(aboa)).TotalSeconds < 120)
                        {
                            string uid = Func.Common.GetLoginUserInfo(HttpContext).UserId;

                            using (var db = new ContextBase())
                            {
                                var sysauth = db.SysAuthorize.Where(x => x.SuId == uid).FirstOrDefault();
                                var isadd = sysauth == null;
                                //新增
                                if (isadd)
                                {
                                    sysauth = new SysAuthorize()
                                    {
                                        SaId = Guid.NewGuid().ToString(),
                                        SuId = uid
                                    };
                                }

                                switch (vtype)
                                {
                                    case "qq":
                                        sysauth.OpenId1 = openId;
                                        break;
                                    case "weibo":
                                        sysauth.OpenId2 = openId;
                                        break;
                                    case "github":
                                        sysauth.OpenId3 = openId;
                                        break;
                                    case "taobao":
                                        sysauth.OpenId4 = openId;
                                        break;
                                    case "microsoft":
                                        sysauth.OpenId5 = openId;
                                        break;
                                }
                                if (isadd)
                                {
                                    db.SysAuthorize.Add(sysauth);
                                }
                                else
                                {
                                    db.SysAuthorize.Update(sysauth);
                                }
                                db.SaveChanges();
                            }

                            Response.Cookies.Delete("AccountBindOAuth");
                            isbind = true;

                            result.code = 301;
                            result.message = "绑定成功";
                            result.url = "/";
                        }
                    }

                    //非绑定操作
                    if (!isbind)
                    {
                        using (var db = new ContextBase())
                        {
                            SysUser vmo = null;
                            switch (vtype)
                            {
                                case "qq":
                                    vmo = (from a in db.SysAuthorize
                                           join b in db.SysUser on a.SuId equals b.SuId
                                           where a.OpenId1 == openId
                                           select b).FirstOrDefault();
                                    break;
                                case "weibo":
                                    vmo = (from a in db.SysAuthorize
                                           join b in db.SysUser on a.SuId equals b.SuId
                                           where a.OpenId2 == openId
                                           select b).FirstOrDefault();
                                    break;
                                case "github":
                                    vmo = (from a in db.SysAuthorize
                                           join b in db.SysUser on a.SuId equals b.SuId
                                           where a.OpenId3 == openId
                                           select b).FirstOrDefault();
                                    break;
                                case "taobao":
                                    vmo = (from a in db.SysAuthorize
                                           join b in db.SysUser on a.SuId equals b.SuId
                                           where a.OpenId4 == openId
                                           select b).FirstOrDefault();
                                    break;
                                case "microsoft":
                                    vmo = (from a in db.SysAuthorize
                                           join b in db.SysUser on a.SuId equals b.SuId
                                           where a.OpenId5 == openId
                                           select b).FirstOrDefault();
                                    break;
                            }

                            //没关联
                            if (vmo == null)
                            {
                                result.code = 302;
                                result.message = "未关联账号，先账号密码登录再关联才能使用";
                            }
                            else
                            {
                                result = await LoginValidation(vmo, "_pass_", 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }

            //成功
            if (result.code == 100)
            {
                return Redirect(result.url);
            }
            else
            {
                return View(result);
            }
        }

        [Description("第三方授权绑定页面")]
        [Authorize]
        public IActionResult AuthBind()
        {
            string uid = Func.Common.GetLoginUserInfo(HttpContext).UserId;
            using (var db = new ContextBase())
            {
                var query = from a in db.SysUser
                            join b in db.SysAuthorize on a.SuId equals b.SuId
                            where a.SuId == uid
                            select b;
                var mo = query.FirstOrDefault();
                if (mo == null)
                {
                    mo = new SysAuthorize();
                }
                return View(mo);
            }
        }

        [Description("解绑第三方授权")]
        [Authorize]
        public string AuthUnBind()
        {
            string result = "fail";
            string vtype = RouteData.Values["id"]?.ToString();
            string uid = Func.Common.GetLoginUserInfo(HttpContext).UserId;
            using (var db = new ContextBase())
            {
                var mo = db.SysAuthorize.Where(x => x.SuId == uid).FirstOrDefault();

                switch (vtype)
                {
                    case "qq":
                        mo.OpenId1 = "";
                        break;
                    case "weibo":
                        mo.OpenId2 = "";
                        break;
                    case "github":
                        mo.OpenId3 = "";
                        break;
                    case "taobao":
                        mo.OpenId4 = "";
                        break;
                    case "microsoft":
                        mo.OpenId5 = "";
                        break;
                }
                db.SysAuthorize.Update(mo);
                db.SaveChanges();
                result = "success";
            }
            return result;
        }

        #endregion

        #region 注销
        [Description("注销")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //清空全局缓存
            Func.Common.GlobalCacheRmove();

            return Redirect("/");
        }
        #endregion

        #region 修改密码
        [Description("修改密码页面")]
        [Authorize]
        public IActionResult UpdatePassword()
        {
            return View();
        }

        /// <summary>
        /// 修改为新的密码
        /// </summary>
        /// <param name="oldpwd">现有</param>
        /// <param name="newpwd1">新</param>
        /// <param name="newpwd2"></param>
        /// <returns></returns>
        [Description("执行修改密码")]
        [Authorize]
        public IActionResult UpdateNewPassword(string oldpwd, string newpwd1, string newpwd2)
        {
            string result = "fail";

            if (string.IsNullOrWhiteSpace(oldpwd) || string.IsNullOrWhiteSpace(newpwd1))
            {
                result = "密码不能为空";
            }
            else if (newpwd1.Length < 5)
            {
                result = "密码长度至少 5 位";
            }
            else if (newpwd1 != newpwd2)
            {
                result = "两次输入的密码不一致";
            }
            else
            {
                var userinfo = Func.Common.GetLoginUserInfo(HttpContext);

                using (var db = new ContextBase())
                {
                    var mo = db.SysUser.Find(userinfo.UserId);
                    if (mo != null && mo.SuPwd == Core.CalcTo.MD5(oldpwd))
                    {
                        mo.SuPwd = Core.CalcTo.MD5(newpwd1);
                        db.SysUser.Update(mo);
                        db.SaveChanges();

                        result = "success";
                    }
                    else
                    {
                        result = "现有密码错误";
                    }
                }
            }

            return Content(result);
        }
        #endregion
    }
}