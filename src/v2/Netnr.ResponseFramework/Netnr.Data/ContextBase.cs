using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace Netnr.Data
{
    public class ContextBase : DbContext
    {
        /// <summary>
        /// 数据库
        /// </summary>
        private DataBase.TypeDB TDB;

        /// <summary>
        /// 应用程序不为每个上下文实例创建新的ILoggerFactory实例非常重要。这样做会导致内存泄漏和性能下降
        /// </summary>
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Error, true)
            });

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (TDB)
            {
                case DataBase.TypeDB.SQLServer:
                    optionsBuilder.UseSqlServer(GlobalVar.GetValue("ConnectionStrings:SQLServerConn"), options =>
                    {
                        //启用 row_number 分页 （兼容2005、2008）
                        //options.UseRowNumberForPaging();
                    });
                    break;
                case DataBase.TypeDB.SQLite:
                    //optionsBuilder.UseSqlite(GlobalVar.GetValue("ConnectionStrings:SQLiteConn"));
                    break;
            }

            //注册日志（修改日志等级为Information，可查看执行的SQL语句）
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        /// <summary>
        /// 上下文
        /// </summary>
        /// <param name="typeDB">数据库类型，默认SQLServer</param>
        public ContextBase(DataBase.TypeDB typeDB = DataBase.TypeDB.SQLServer) : base()
        {
            TDB = typeDB;
        }

        /// <summary>
        /// 反射注册实体类
        /// 
        /// DomainMapping  所在的程序集一定要写对，因为目前在当前项目所以是采用的当前正在运行的程序集。
        /// 如果你的mapping在单独的项目中 记得要加载对应的assembly 这是重点
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //实体映射模块名称
            string MapNamespace = "Netnr.Mapping.dll";
            string path = GlobalVar.ContentRootPath + "/" + MapNamespace;

            var modelObject = System.Reflection.Assembly.LoadFile(path).GetTypes();

            foreach (var mo in modelObject)
            {
                if (mo.Name != "<>c")
                {
                    //modelBuilder.Model.AddEntityType(mo);
                    dynamic configurationInstance = Activator.CreateInstance(mo);
                    modelBuilder.ApplyConfiguration(configurationInstance);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}

