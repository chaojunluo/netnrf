using System.IO;
using System.Web;

namespace Netnr.Core
{
    public class DownTo
    {
        /// <summary>
        /// 流的方式下载
        /// </summary>
        public static void Stream(string path, string fileName)
        {
            FileStream fileStream = new FileStream(path + fileName, FileMode.Open);
            byte[] bytes = new byte[(int)fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            var Response = HttpContext.Current.Response;
            Response.ContentType = "application/octet-stream";

            // 通知浏览器下载而不是打开  
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));

            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}