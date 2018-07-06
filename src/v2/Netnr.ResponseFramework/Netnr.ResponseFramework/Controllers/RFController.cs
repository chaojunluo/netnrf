using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Netnr.ResponseFramework.Controllers
{
    public class RFController : Controller
    {
        /// <summary>
        /// 表配置示例
        /// </summary>
        /// <returns></returns>
        public IActionResult Tce()
        {
            return View();
        }

        public IActionResult DataGrid()
        {
            return View();
        }

        public IActionResult TreeGrid(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                using (var ru = new Data.RepositoryUse())
                {
                    var query = from a in ru.Context.Set<Domain.SysMenu>()
                                where a.PID == id
                                orderby a.MenuOrder
                                select new
                                {
                                    a.ID,
                                    a.PID,
                                    a.Name,
                                    a.Url,
                                    a.MenuOrder,
                                    a.Icon,
                                    a.Status,
                                    a.MenuGroup,
                                    //查询是否有子集
                                    state = (from b in ru.Context.Set<Domain.SysMenu>() where b.PID == a.ID select b.ID).Count() > 0 ? "closed" : "open"
                                };
                    var list = query.ToList();
                    return Content(list.ToJson());
                }
            }

            return View();
        }

        public IActionResult PropertyGrid()
        {
            return View();
        }

        public IActionResult Modal()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

    }
}