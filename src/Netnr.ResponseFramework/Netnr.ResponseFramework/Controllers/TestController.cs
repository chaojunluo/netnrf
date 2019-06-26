using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 测试控制器，可随意删除任意方法
    /// </summary>
    [AllowAnonymous]
    public class TestController : Controller
    {
        public ActionResultVM Index()
        {
            var vm = new ActionResultVM();


            return vm;
        }
    }
}