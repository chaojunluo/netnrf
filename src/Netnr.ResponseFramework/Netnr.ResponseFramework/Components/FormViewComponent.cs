using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netnr.Data;
using Netnr.Func.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace Netnr.ResponseFramework.Components
{
    public class FormViewComponent : ViewComponent
    {
        /// <summary>
        /// 表单组件
        /// </summary>
        /// <param name="vm">表单组件参数</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(InvokeFormVM vm)
        {
            using (var db = new ContextBase())
            {
                //查询表对应的表单，排序，按区域分组
                var query = from a in db.SysTableConfig
                            where a.TableName == vm.TableName
                            orderby a.FormOrder ascending
                            group a by a.FormArea.Value into g
                            select g;

                var list = await query.ToListAsync();
                vm.Data = list.OrderBy(x => x.Key).ToList();

                return View(vm.ViewName, vm);
            }
        }
    }
}
