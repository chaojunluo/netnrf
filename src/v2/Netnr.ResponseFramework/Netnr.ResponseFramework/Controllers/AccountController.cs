using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    public class AccountController : Controller
    {
        #region 登录

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public FileResult Captcha()
        {
            string num = Core.RandomTo.StrCode(4);
            HttpContext.Session.SetString("captcha", num);
            byte[] bytes = Core.ImageTo.CreateImg(num);
            return File(bytes, "image/jpeg");
        }

        public IActionResult Login()
        {
            var context = HttpContext;
            var loginUser = new LoginUserVM
            {
                UserId = context.User.FindFirst(ClaimTypes.Sid)?.Value,
                UserName = context.User.FindFirst(ClaimTypes.Name)?.Value,
                Nickname = context.User.FindFirst(ClaimTypes.GivenName)?.Value,
                RoleId = context.User.FindFirst(ClaimTypes.Role)?.Value
            };

            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="mo"></param>
        /// <param name="captcha">验证码</param>
        /// <param name="remember">1记住登录状态</param>
        /// <returns></returns>
        public async Task<AccountValidationVM> LoginValidation(SysUser mo, string captcha, int remember)
        {
            var result = new AccountValidationVM();

            if (string.IsNullOrWhiteSpace(captcha) || HttpContext.Session.GetString("captcha").ToLower() != captcha.ToLower())
            {
                result.code = 104;
                result.message = "验证码错误或已过期";
                return result;
            }

            var outMo = new SysUser();

            if (string.IsNullOrWhiteSpace(mo.UserName) || string.IsNullOrWhiteSpace(mo.UserPwd))
            {
                result.code = 101;
                result.message = "用户名或密码不能为空";
                return result;
            }
            else
            {
                using (var ru = new RepositoryUse())
                {
                    outMo = ru.SysUserRepository.IQueryable(x => x.UserName == mo.UserName && x.UserPwd == Core.CalcTo.MD5(mo.UserPwd, 32)).FirstOrDefault();
                }
            }

            if (outMo == null || string.IsNullOrWhiteSpace(outMo.ID))
            {
                result.code = 102;
                result.message = "用户名或密码错误";
                return result;
            }

            if (outMo.Status != 1)
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
                identity.AddClaim(new Claim(ClaimTypes.Sid, outMo.ID));
                identity.AddClaim(new Claim(ClaimTypes.Name, outMo.UserName));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, outMo.Nickname ?? ""));
                identity.AddClaim(new Claim(ClaimTypes.Role, outMo.RoleID));
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

        #region 注销

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //清空全局缓存
            Func.Common.GlobalCacheRmove();

            return Redirect("/");
        }

        #endregion

        #region 修改密码

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

                using (var ru = new RepositoryUse())
                {
                    var mo = ru.SysUserRepository.FindEntity(userinfo.UserId);
                    if (mo != null && mo.UserPwd == Core.CalcTo.MD5(oldpwd))
                    {
                        mo.UserPwd = Core.CalcTo.MD5(newpwd1);
                        ru.Save();
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