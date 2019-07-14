using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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

            #region 第三方登录（如果不用，请以最快的速度删了，^_^）
            QQConfig.APPID = GlobalTo.GetValue("OAuthLogin:QQ:APPID");
            QQConfig.APPKey = GlobalTo.GetValue("OAuthLogin:QQ:APPKey");
            QQConfig.Redirect_Uri = GlobalTo.GetValue("OAuthLogin:QQ:Redirect_Uri");

            WeiboConfig.AppKey = GlobalTo.GetValue("OAuthLogin:Weibo:AppKey");
            WeiboConfig.AppSecret = GlobalTo.GetValue("OAuthLogin:Weibo:AppSecret");
            WeiboConfig.Redirect_Uri = GlobalTo.GetValue("OAuthLogin:Weibo:Redirect_Uri");

            GitHubConfig.ClientID = GlobalTo.GetValue("OAuthLogin:GitHub:ClientID");
            GitHubConfig.ClientSecret = GlobalTo.GetValue("OAuthLogin:GitHub:ClientSecret");
            GitHubConfig.Redirect_Uri = GlobalTo.GetValue("OAuthLogin:GitHub:Redirect_Uri");
            GitHubConfig.ApplicationName = GlobalTo.GetValue("OAuthLogin:GitHub:ApplicationName");

            TaobaoConfig.AppKey = GlobalTo.GetValue("OAuthLogin:Taobao:AppKey");
            TaobaoConfig.AppSecret = GlobalTo.GetValue("OAuthLogin:Taobao:AppSecret");
            TaobaoConfig.Redirect_Uri = GlobalTo.GetValue("OAuthLogin:Taobao:Redirect_Uri");

            MicroSoftConfig.ClientID = GlobalTo.GetValue("OAuthLogin:MicroSoft:ClientID");
            MicroSoftConfig.ClientSecret = GlobalTo.GetValue("OAuthLogin:MicroSoft:ClientSecret");
            MicroSoftConfig.Redirect_Uri = GlobalTo.GetValue("OAuthLogin:MicroSoft:Redirect_Uri");
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
                //注册过滤器
                options.Filters.Add(new Filters.FilterConfigs.ErrorActionFilter());
                options.Filters.Add(new Filters.FilterConfigs.GlobalActionAttribute());
            }).AddJsonOptions(options =>
            {
                //Action原样输出JSON
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                //日期格式化
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
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

            //定时任务
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, Controllers.ServicesController.TaskService>();

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

            //全局限制请求大小，上传文件大小限制（详细信息：FormOptions）
            services.Configure<FormOptions>(options =>
            {
                //50MB
                options.ValueLengthLimit = 1024 * 1024 * 50;
                options.MultipartBodyLengthLimit = options.ValueLengthLimit;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMemoryCache memoryCache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //默认起始页index.html
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //授权访问
            app.UseAuthentication();

            //session
            app.UseSession();

            //缓存
            Core.CacheTo.memoryCache = memoryCache;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
