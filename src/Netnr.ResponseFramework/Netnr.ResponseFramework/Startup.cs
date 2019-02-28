using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netnr.Data;
using Netnr.Login;

namespace Netnr.ResponseFramework
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            GlobalTo.Configuration = configuration;
            GlobalTo.HostingEnvironment = env;

            #region 第三方登录配置（如果不用，请以最快的速度删了，^_^）
            //也可把秘钥配置到appsettings.json，如：GlobalVar.GetValue("Login:QQConfig:APPID")

            QQConfig.APPID = "101511640";
            QQConfig.APPKey = "f26b4af4a9d68bec2bbcbeee443feb83";
            QQConfig.Redirect_Uri = "https://rf2.netnr.com/account/authcallback/qq";

            WeiboConfig.AppKey = "";
            WeiboConfig.AppSecret = "";
            WeiboConfig.Redirect_Uri = "";

            GitHubConfig.ClientID = "73cd8c706b166968db5b";
            GitHubConfig.ClientSecret = "7e0495dff8e34e07b37b19b1f8762a36d4bb07a7";
            GitHubConfig.Redirect_Uri = "https://rf2.netnr.com/account/authcallback/github";
            GitHubConfig.ApplicationName = "netnrf";

            TaobaoConfig.AppKey = "25163813";
            TaobaoConfig.AppSecret = "c3b45e2605aa8696fb5e8d399fd0ca54";
            TaobaoConfig.Redirect_Uri = "https://rf2.netnr.com/account/authcallback/taobao";

            MicroSoftConfig.ClientID = "6b5f41be-975d-48a4-a971-950bb01097b4";
            MicroSoftConfig.ClientSecret = "ttzJRE0;()xgmdPQKC3211^";
            MicroSoftConfig.Redirect_Uri = "https://rf2.netnr.com/account/authcallback/microsoft";
            #endregion

            //无创建，有忽略
            using (var db = new ContextBase())
            {
                //不存在创建，创建后返回true
                if (db.Database.EnsureCreated())
                {
                    //调用重置数据库（实际开发中，你可能不需要，或只初始化一些表数据）
                    new Controllers.ToolController().ResetDataBase();
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                //cookie存储需用户同意，欧盟新标准，暂且关闭，否则用户没同意无法写入
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc(options =>
            {
                //注册全局过滤器
                options.Filters.Add(new Filters.FilterConfigs.ErrorActionFilter());

                options.Filters.Add(new Filters.FilterConfigs.LogActionAttribute());
            });

            //授权访问信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "netnrf_auth";
                options.LoginPath = new PathString("/account/login");
                options.AccessDeniedPath = new PathString("/account/login");
                options.ExpireTimeSpan = DateTime.Now.AddDays(10) - DateTime.Now;
            });

            //session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "netnrf_session";
                //5分钟过期
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
            });

            //跨域（ 用法：[EnableCors("Any")] ）
            services.AddCors(options =>
            {
                options.AddPolicy("Any", builder =>
                {
                    //允许任何来源的主机访问
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                    //指定处理cookie
                });
            });

            //配置上传文件大小限制（详细信息：FormOptions）
            //services.Configure<FormOptions>(optioins =>
            //{
            //    //20MB
            //    optioins.ValueLengthLimit = 1024 * 1024 * 20;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMemoryCache memoryCache)
        {
            //缓存
            Core.CacheTo.memoryCache = memoryCache;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //授权访问
            app.UseAuthentication();

            //session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
