using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netnr.Data;
using Netnr.Domain;
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
        public FunctionResultVM Export(QueryDataVM.GetParams param, string title)
        {
            var vm = new FunctionResultVM();

            //表配置
            var listColumn = new List<SysTableConfig>();

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
                var db = new ContextBase();

                switch (param.uri?.ToLower())
                {
                    default:
                        vm.Set(FRTag.invalid);
                        break;

                    //角色
                    case "sysrole":
                        {
                            var query = from a in db.SysRole select a;

                            var listModels = ExportAid.QueryJoin(query, param, db, out listColumn);
                            dtReport = ExportAid.ModelsMapping(param.uri, listModels, listColumn);
                        }
                        break;

                    //用户
                    case "sysuser":
                        {
                            var query = from a in db.SysUser
                                        join b in db.SysRole on a.SrId equals b.SrId
                                        select new
                                        {
                                            a.SuId,
                                            a.SrId,
                                            a.SuName,
                                            a.SuPwd,
                                            a.SuNickname,
                                            a.SuCreateTime,
                                            a.SuStatus,
                                            a.SuSign,
                                            a.SuGroup,

                                            b.SrName
                                        };

                            var listModels = ExportAid.QueryJoin(query, param, db, out listColumn);
                            dtReport = ExportAid.ModelsMapping(param.uri, listModels, listColumn);
                        }
                        break;

                    //日志
                    case "syslog":
                        {
                            var query = from a in db.SysLog select a;

                            var listModels = ExportAid.QueryJoin(query, param, db, out listColumn);
                            dtReport = ExportAid.ModelsMapping(param.uri, listModels, listColumn);
                        }
                        break;
                }

                db.Dispose();

                if (vm.message != FRTag.invalid.ToString())
                {
                    //生成
                    if (Fast.NpoiTo.DataTableToExcel(dtReport, vpath + filename))
                    {
                        vm.data = path + filename;

                        //生成的Excel继续操作
                        ExportAid.ExcelDraw(vpath + filename, param.uri?.ToLower());

                        vm.Set(FRTag.success);
                    }
                    else
                    {
                        vm.Set(FRTag.fail);
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