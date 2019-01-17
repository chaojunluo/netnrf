using Netnr.Core;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

public class GlobalVar
{
    /// <summary>
    /// 全局配置
    /// </summary>
    //public static IConfiguration Configuration;

    /// <summary>
    /// 托管环境信息
    /// 
    /// //内部访问（项目根路径）
    /// HostingEnvironment.ContentRootPath
    /// 
    /// //web外部访问（wwwroot）
    /// HostingEnvironment.WebRootPath
    /// 
    /// </summary>
    //public static IHostingEnvironment HostingEnvironment;

    /// 在Windows环境中上面的 配置、环境 就够用，而linux中，有问题，思路是：反射拿到dotnet运行目录

    /// <summary>
    /// 内部访问（项目根路径）
    /// </summary>
    private static string contentRootPath;
    public static string ContentRootPath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(contentRootPath))
            {
                var path = Path.GetDirectoryName(new GlobalVar().GetType().Assembly.Location);
                contentRootPath = path;
            }
            return contentRootPath;
        }
        set => contentRootPath = value;
    }

    /// <summary>
    /// web外部访问（wwwroot）
    /// </summary>
    private static string webRootPath;
    public static string WebRootPath
    {
        get
        {
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = ContentRootPath + "/wwwroot";
            }
            return webRootPath;
        }
        set => webRootPath = value;
    }

    /// <summary>
    /// json配置文件，需AppsettingsJson复制到输出目录，不然调试找到文件
    /// </summary>
    private static JObject appsettingsJson;
    public static JObject AppsettingsJson
    {
        get
        {
            if (appsettingsJson == null)
            {
                string appJson = FileTo.ReadText(ContentRootPath + "/", "appsettings.json");
                appsettingsJson = appJson.ToJObject();
            }
            return appsettingsJson;
        }
        set => appsettingsJson = value;
    }

    /// <summary>
    /// 起始路径（Windows为磁盘跟目录，linux为/）
    /// </summary>
    public static string StartPath = "/";

    /// <summary>
    /// 获取AppsettingsJson的值
    /// </summary>
    /// <param name="path">如：ConnectionStrings:SQLServerConn</param>
    /// <returns></returns>
    public static string GetValue(string path)
    {
        string result = string.Empty;

        if (!string.IsNullOrWhiteSpace(path))
        {
            var listp = path.Split(':').ToList();
            var deep = 0;
            var jo = AppsettingsJson as JToken;
            while (deep < listp.Count)
            {
                try
                {
                    jo = jo[listp[deep++]];
                }
                catch (System.Exception)
                {
                    goto output;
                }
            }
            result = jo.ToStringOrEmpty();
        }
        output: return result;
    }
}