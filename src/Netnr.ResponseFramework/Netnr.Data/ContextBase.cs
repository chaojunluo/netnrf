using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Netnr.Data
{
    /// <summary>
    /// ContextBase 连接
    /// </summary>
    public partial class ContextBase : DbContext
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public enum TypeDB
        {
            MySQL,
            SQLite,
            SQLServer,
            PostgreSQL
        }

        /// <summary>
        /// 数据库
        /// </summary>
        public readonly TypeDB TDB;

        /// <summary>
        /// 上下文
        /// </summary>
        public ContextBase() : base()
        {
            TDB = (TypeDB)Enum.Parse(typeof(TypeDB), GlobalTo.GetValue("TypeDB"), true);
        }

        /// <summary>
        /// 应用程序不为每个上下文实例创建新的ILoggerFactory实例非常重要。这样做会导致内存泄漏和性能下降
        /// </summary>
        private static ILoggerFactory _loggerFactory = null;
        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_loggerFactory == null)
                {
                    var sc = new ServiceCollection();
                    sc.AddLogging(builder => builder.AddConsole().AddFilter(level => level >= LogLevel.Information));
                    _loggerFactory = sc.BuildServiceProvider().GetService<ILoggerFactory>();
                }
                return _loggerFactory;
            }
        }

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (TDB)
            {
                case TypeDB.MySQL:
                    optionsBuilder.UseMySql(GlobalTo.GetValue("ConnectionStrings:MySQLConn"));
                    break;
                case TypeDB.SQLite:
                    optionsBuilder.UseSqlite(GlobalTo.GetValue("ConnectionStrings:SQLiteConn"));
                    break;
                case TypeDB.SQLServer:
                    optionsBuilder.UseSqlServer(GlobalTo.GetValue("ConnectionStrings:SQLServerConn"), options =>
                    {
                        //启用 row_number 分页 （兼容2005、2008）
                        //options.UseRowNumberForPaging();
                    });
                    break;
                case TypeDB.PostgreSQL:
                    optionsBuilder.UseNpgsql(GlobalTo.GetValue("ConnectionStrings:PostgreSQLConn"));
                    break;
            }

            //注册日志（修改日志等级为Information，可查看执行的SQL语句）
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }
    }
}