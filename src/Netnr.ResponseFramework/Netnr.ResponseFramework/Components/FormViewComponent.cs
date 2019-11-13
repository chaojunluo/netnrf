using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netnr.Data;
using Netnr.Func.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace Netnr.ResponseFramework.Components
{
    /// <summary>
    /// 表单视图组件
    /// </summary>
    public class FormViewComponent : ViewComponent
    {
        /// <summary>
        /// 表单、单据 组件
        /// </summary>
        /// <param name="vm">表单组件参数</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(InvokeFormVM vm)
        {
            using var db = new ContextBase();
            //查询表对应的表单，排序，按区域分组
            var list = await db.SysTableConfig
               .Where(x => x.TableName == vm.TableName)
               .ToListAsync();

            vm.Data = list.OrderBy(x => x.FormOrder)
                .GroupBy(x => x.FormArea.Value)
                .OrderBy(x => x.Key)
                .ToList();

            return View(vm.ViewName, vm);
        }
    }
}
