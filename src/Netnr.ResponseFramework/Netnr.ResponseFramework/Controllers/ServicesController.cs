using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Netnr.Func.ViewModel;

namespace Netnr.ResponseFramework.Controllers
{
    /// <summary>
    /// 服务
    /// </summary>
    public class ServicesController : Controller
    {
        #region 定时任务

        [Description("执行任务")]
        public FunctionResultVM ExecTask(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = RouteData.Values["id"]?.ToString();
            }

            var vm = new FunctionResultVM();

            try
            {
                switch (id)
                {
                    default:
                        vm.Set(FRTag.invalid);
                        break;

                    //重置数据库
                    case "resetdatabase":
                        {
                            vm.data = new ToolController().ResetDataBase();
                            vm.Set(FRTag.success);
                        }
                        break;

                    //清理临时目录
                    case "cleartemp":
                        {
                            string directoryPath = (GlobalTo.WebRootPath + "/upload/temp/").Replace("\\", "/");

                            //删除文件
                            var listFile = Directory.GetFiles(directoryPath).ToList();
                            foreach (var path in listFile)
                            {
                                if (!path.Contains("README"))
                                {
                                    System.IO.File.Delete(path);
                                }
                            }

                            //删除文件夹
                            var listFolder = Directory.GetDirectories(directoryPath).ToList();
                            foreach (var path in listFolder)
                            {
                                Directory.Delete(path, true);
                            }

                            vm.data = $"删除文件{listFile.Count}个，删除{listFolder.Count}个文件夹";
                            vm.Set(FRTag.success);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                vm.Set(ex);
            }

            return vm;
        }

        /// <summary>
        /// 定时任务服务
        /// </summary>
        public class TaskService : BackgroundService
        {
            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var sc = new ServicesController();

                        //执行结果
                        var vm = new FunctionResultVM();
                        vm.code = -9;

                        //重置一次数据库
                        if (DateTime.Now.Day % 2 == 0 && DateTime.Now.ToString("HHmm") == "0404")
                        {
                            vm = sc.ExecTask("resetdatabase");
                        }

                        //清理临时目录
                        if (DateTime.Now.Day % 2 == 0 && DateTime.Now.ToString("HHmm") == "0303")
                        {
                            vm = sc.ExecTask("cleartemp");
                        }

                        //记录任务日志
                        if (vm.code != -9)
                        {
                            string msg = "Tasking：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + vm.ToJson();
                            Core.ConsoleTo.Log(msg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Core.ConsoleTo.Log(ex);
                    }

                    //1分钟
                    await Task.Delay(1000 * 60 * 1, stoppingToken);
                }
            }
        }
        #endregion
    }
}