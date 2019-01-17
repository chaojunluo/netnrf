using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using System.Linq;

namespace Netnr.ResponseFramework.Components
{
    public class FormViewComponent : ViewComponent
    {
        /// <summary>
        /// 模态框表单组件
        /// </summary>
        /// <param name="tablename">列配置表名</param>
        /// <param name="paneltitle">面板标题（有两个区域或以上时，英文逗号分隔）</param>
        /// <param name="modalsize">模态框大小 1小 2中 3大</param>
        /// <param name="index">表单ID后缀</param>
        /// <returns></returns>
        public IViewComponentResult Invoke(string tablename, string paneltitle = "", int modalsize = 3, int index = 1)
        {
            using (var db = new ContextBase())
            {
                //查询表对应的表单，排序，按区域分组
                var listCol = db.SysTableConfig.Where(x => x.TableName == tablename).OrderBy(x => x.FormOrder).ToList();

                var groupCol = listCol.GroupBy(x => x.FormArea.Value).ToList();

                //表名
                TempData["tablename"] = tablename;
                //ID索引
                TempData["index"] = index;
                //模态框大小
                TempData["modalsize"] = modalsize == 3 ? "modal-lg" : modalsize == 1 ? "modal-sm" : "";
                //多面板标题
                TempData["paneltitle"] = paneltitle.Split(',').ToList();

                return View(groupCol);
            }
        }
    }
}
