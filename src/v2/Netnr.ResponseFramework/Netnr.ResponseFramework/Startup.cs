using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netnr.Data;

namespace Netnr.ResponseFramework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //无创建，有忽略
            using (var db = new ContextBase())
            {
                db.Database.EnsureCreated();
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(options =>
            {
                //注册全局过滤器
                options.Filters.Add(new Global.FilterConfigs.ErrorActionFilter());

                options.Filters.Add(new Filters.FilterConfigs.LogActionAttribute());
            });

            //授权访问信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/account/login");
                options.AccessDeniedPath = new PathString("/account/login");
                options.ExpireTimeSpan = System.DateTime.Now.AddDays(10) - System.DateTime.Now;
            });

            //session
            services.AddSession();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
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

            //缓存
            Core.CacheTo.memoryCache = memoryCache;
        }
    }
}
