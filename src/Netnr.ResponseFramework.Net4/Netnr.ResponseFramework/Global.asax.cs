using Netnr.Data;
using Netnr.Login;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Netnr.ResponseFramework
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

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
                if (!db.Database.Exists())
                {
                    db.Database.Initialize(true);

                    //调用重置数据库（实际开发中，你可能不需要，或只初始化一些表数据）
                    new Controllers.ToolController().ResetDataBase();
                }
            }
        }
    }
}