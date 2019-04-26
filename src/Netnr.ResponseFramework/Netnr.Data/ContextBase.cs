using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Netnr.Domain;

namespace Netnr.Data
{
    public class ContextBase : DbContext
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
        /// <param name="typeDB">数据库类型</param>
        public ContextBase(TypeDB typeDB = TypeDB.SQLServer) : base()
        {
            TDB = typeDB;
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
                    sc.AddLogging(builder => builder.AddConsole().AddFilter(level => level >= LogLevel.Warning));
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
                    optionsBuilder.UseNpgsql(GlobalTo.GetValue("ConnectionStrings:PostgreSQL"));
                    break;
            }

            //注册日志（修改日志等级为Information，可查看执行的SQL语句）
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

        public virtual DbSet<SysAuthorize> SysAuthorize { get; set; }
        public virtual DbSet<SysButton> SysButton { get; set; }
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysTableConfig> SysTableConfig { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<TempExample> TempExample { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<SysAuthorize>(entity =>
            {
                entity.HasKey(e => e.SaId)
                    .HasName("SysAuthorize_SaId")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.SuId)
                    .HasName("SysAuthorize_SuId_PK")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.SaId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.OpenId1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId5)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId6)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId7)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId8)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpenId9)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysButton>(entity =>
            {
                entity.HasKey(e => e.SbId)
                    .HasName("SysButton_SbId_PK");

                entity.Property(e => e.SbId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SbBtnClass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SbBtnIcon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SbBtnId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SbBtnText).HasMaxLength(20);

                entity.Property(e => e.SbDescribe).HasMaxLength(200);

                entity.Property(e => e.SbPid)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("SysLog_LogId_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.LogCreateTime)
                    .HasName("SysLog_LogCreateTime")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.LogId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LogAction).HasMaxLength(50);

                entity.Property(e => e.LogBrowserName).HasMaxLength(50);

                entity.Property(e => e.LogContent).HasMaxLength(200);

                entity.Property(e => e.LogCreateTime).HasColumnType("datetime");

                entity.Property(e => e.LogIp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogRemark).HasMaxLength(200);

                entity.Property(e => e.LogSystemName).HasMaxLength(50);

                entity.Property(e => e.LogUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SuName).HasMaxLength(50);

                entity.Property(e => e.SuNickname).HasMaxLength(50);
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasKey(e => e.SmId)
                    .HasName("SysMenu_SmId_PK");

                entity.Property(e => e.SmId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SmIcon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SmName).HasMaxLength(50);

                entity.Property(e => e.SmPid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SmUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.SrId)
                    .HasName("SysRole_SrId_PK");

                entity.Property(e => e.SrId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SrCreateTime).HasColumnType("datetime");

                entity.Property(e => e.SrDescribe).HasMaxLength(200);

                entity.Property(e => e.SrName).HasMaxLength(200);

                entity.Property(e => e.SrStatus).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SysTableConfig>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SysTableConfig_Id_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.TableName)
                    .HasName("SysTableConfig_TableName")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ColExport).HasDefaultValueSql("((0))");

                entity.Property(e => e.ColField)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ColFormat).HasMaxLength(200);

                entity.Property(e => e.ColFrozen).HasDefaultValueSql("((0))");

                entity.Property(e => e.ColOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.ColQuery).HasDefaultValueSql("((0))");

                entity.Property(e => e.ColSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.ColTitle).HasMaxLength(200);

                entity.Property(e => e.DvTitle).HasMaxLength(200);

                entity.Property(e => e.FormPlaceholder).HasMaxLength(200);

                entity.Property(e => e.FormRequired).HasDefaultValueSql("((0))");

                entity.Property(e => e.FormType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.SuId)
                    .HasName("SysUser_SuId_PK");

                entity.Property(e => e.SuId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SrId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuCreateTime).HasColumnType("datetime");

                entity.Property(e => e.SuName).HasMaxLength(50);

                entity.Property(e => e.SuNickname).HasMaxLength(50);

                entity.Property(e => e.SuPwd).HasMaxLength(50);

                entity.Property(e => e.SuSign)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuStatus).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TempExample>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ColField)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ColFormat).HasMaxLength(200);

                entity.Property(e => e.ColTitle).HasMaxLength(200);

                entity.Property(e => e.DvTitle).HasMaxLength(200);

                entity.Property(e => e.FormPlaceholder).HasMaxLength(200);

                entity.Property(e => e.FormType)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}

