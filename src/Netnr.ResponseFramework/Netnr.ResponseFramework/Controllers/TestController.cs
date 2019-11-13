using Microsoft.AspNetCore.Mvc;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : Controller
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Content("");
        }
    }
}