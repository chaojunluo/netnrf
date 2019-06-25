using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Func;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 输入输出
    /// </summary>
    [Authorize]
    public class IOController : Controller
    {
        #region 导出

        [Description("公共导出")]
        public ActionResultVM Export(QueryDataInputVM ivm, string title)
        {
            var vm = new ActionResultVM();

            //文件路径
            string path = "/upload/temp/";
            var vpath = (GlobalTo.WebRootPath + path).Replace("\\", "/");
            if (!Directory.Exists(vpath))
            {
                Directory.CreateDirectory(vpath);
            }

            //文件名
            string filename = title.Replace(" ", "").Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            //导出的表数据
            var dtReport = new DataTable();

            try
            {
                var ovm = new QueryDataOutputVM();

                var db = new ContextBase();

                switch (ivm.tableName?.ToLower())
                {
                    default:
                        vm.Set(ARTag.invalid);
                        break;

                    //角色
                    case "sysrole":
                        {
                            ovm = new SettingController().QuerySysRole(ivm);
                            dtReport = ExportAid.ModelsMapping(ivm, ovm);
                        }
                        break;

                    //用户
                    case "sysuser":
                        {
                            ovm = new SettingController().QuerySysUser(ivm);
                            dtReport = ExportAid.ModelsMapping(ivm, ovm);
                        }
                        break;

                    //日志
                    case "syslog":
                        {
                            ovm = new SettingController().QuerySysLog(ivm);
                            dtReport = ExportAid.ModelsMapping(ivm, ovm);
                        }
                        break;

                    //字典
                    case "sysdictionary":
                        {
                            ovm = new SettingController().QuerySysDictionary(ivm);
                            dtReport = ExportAid.ModelsMapping(ivm, ovm);
                        }
                        break;
                }

                db.Dispose();

                if (vm.msg != ARTag.invalid.ToString())
                {
                    //生成
                    if (Fast.NpoiTo.DataTableToExcel(dtReport, vpath + filename))
                    {
                        vm.data = path + filename;

                        //生成的Excel继续操作
                        ExportAid.ExcelDraw(vpath + filename, ivm);

                        vm.Set(ARTag.success);
                    }
                    else
                    {
                        vm.Set(ARTag.fail);
                    }
                }
            }
            catch (Exception ex)
            {
                vm.Set(ex);
            }

            return vm;
        }

        [Description("导出下载")]
        public void ExportDown(string path)
        {
            path = GlobalTo.ContentRootPath + path;
            new Core.DownTo(Response).Stream(path, "");
        }

        #endregion
    }
}