using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
            MySql,
            SQLite,
            SQLServer
        }

        /// <summary>
        /// 数据库
        /// </summary>
        public readonly TypeDB TDB;

        /// <summary>
        /// 上下文
        /// </summary>
        /// <param name="typeDB">数据库类型</param>
        public ContextBase(TypeDB typeDB = TypeDB.MySql) : base()
        {
            TDB = typeDB;
        }

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
                case TypeDB.SQLServer:
                    optionsBuilder.UseSqlServer(GlobalVar.GetValue("ConnectionStrings:SQLServerConn"), options =>
                    {
                        //启用 row_number 分页 （兼容2005、2008）
                        //options.UseRowNumberForPaging();
                    });
                    break;
                case TypeDB.MySql:
                    optionsBuilder.UseMySql(GlobalVar.GetValue("ConnectionStrings:MySqlConn"));
                    break;
                case TypeDB.SQLite:
                    optionsBuilder.UseSqlite(GlobalVar.GetValue("ConnectionStrings:SQLiteConn"));
                    break;
            }

            //注册日志（修改日志等级为Information，可查看执行的SQL语句）
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        public virtual DbSet<SysButton> SysButton { get; set; }
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysTableConfig> SysTableConfig { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<TempExample> TempExample { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysButton>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.HasIndex(e => e.Pid)
                    .HasName("Pid");

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.BtnClass).HasColumnType("varchar(50)");

                entity.Property(e => e.BtnGroup).HasColumnType("int(11)");

                entity.Property(e => e.BtnIcon).HasColumnType("varchar(50)");

                entity.Property(e => e.BtnId).HasColumnType("varchar(50)");

                entity.Property(e => e.BtnOrder).HasColumnType("int(11)");

                entity.Property(e => e.BtnText).HasColumnType("varchar(20)");

                entity.Property(e => e.Describe).HasColumnType("varchar(200)");

                entity.Property(e => e.Pid).HasColumnType("varchar(50)");

                entity.Property(e => e.Status).HasColumnType("int(11)");
            });

            modelBuilder.Entity<SysLog>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.Action).HasColumnType("varchar(50)");

                entity.Property(e => e.BrowserName).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Ip).HasColumnType("varchar(50)");

                entity.Property(e => e.LogContent).HasColumnType("varchar(200)");

                entity.Property(e => e.LogGroup).HasColumnType("int(11)");

                entity.Property(e => e.Nickname).HasColumnType("varchar(50)");

                entity.Property(e => e.Remark).HasColumnType("varchar(200)");

                entity.Property(e => e.SystemName).HasColumnType("varchar(50)");

                entity.Property(e => e.Url).HasColumnType("varchar(255)");

                entity.Property(e => e.UserName).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.HasIndex(e => e.Pid)
                    .HasName("Pid");

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.Icon).HasColumnType("varchar(50)");

                entity.Property(e => e.MenuGroup).HasColumnType("int(11)");

                entity.Property(e => e.MenuOrder).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.Property(e => e.Pid).HasColumnType("varchar(50)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Url).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.Buttons).HasColumnType("longtext");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasColumnType("varchar(200)");

                entity.Property(e => e.Menus).HasColumnType("longtext");

                entity.Property(e => e.Name).HasColumnType("varchar(200)");

                entity.Property(e => e.RoleGroup).HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<SysTableConfig>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.HasIndex(e => e.TableName)
                    .HasName("TableName");

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.ColAlign).HasColumnType("int(11)");

                entity.Property(e => e.ColExport)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ColField).HasColumnType("varchar(200)");

                entity.Property(e => e.ColFormat).HasColumnType("varchar(200)");

                entity.Property(e => e.ColFrozen)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ColHide).HasColumnType("int(11)");

                entity.Property(e => e.ColOrder).HasColumnType("int(11)");

                entity.Property(e => e.ColQuery)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ColSort)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ColTitle).HasColumnType("varchar(200)");

                entity.Property(e => e.ColWidth).HasColumnType("int(11)");

                entity.Property(e => e.DvTitle).HasColumnType("varchar(200)");

                entity.Property(e => e.FormArea).HasColumnType("int(11)");

                entity.Property(e => e.FormHide).HasColumnType("int(11)");

                entity.Property(e => e.FormOrder).HasColumnType("int(11)");

                entity.Property(e => e.FormPlaceholder).HasColumnType("varchar(200)");

                entity.Property(e => e.FormRequired)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FormSpan).HasColumnType("int(11)");

                entity.Property(e => e.FormText).HasColumnType("longtext");

                entity.Property(e => e.FormType).HasColumnType("varchar(200)");

                entity.Property(e => e.FormUrl).HasColumnType("longtext");

                entity.Property(e => e.FormValue).HasColumnType("longtext");

                entity.Property(e => e.TableName).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Nickname).HasColumnType("varchar(50)");

                entity.Property(e => e.RoleId).HasColumnType("varchar(50)");

                entity.Property(e => e.Sign).HasColumnType("varchar(50)");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserGroup).HasColumnType("int(11)");

                entity.Property(e => e.UserName).HasColumnType("varchar(50)");

                entity.Property(e => e.UserPwd).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TempExample>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Id");

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.ColAlign).HasColumnType("int(11)");

                entity.Property(e => e.ColExport).HasColumnType("int(11)");

                entity.Property(e => e.ColField).HasColumnType("varchar(200)");

                entity.Property(e => e.ColFormat).HasColumnType("varchar(200)");

                entity.Property(e => e.ColFrozen).HasColumnType("int(11)");

                entity.Property(e => e.ColHide).HasColumnType("int(11)");

                entity.Property(e => e.ColOrder).HasColumnType("int(11)");

                entity.Property(e => e.ColQuery).HasColumnType("int(11)");

                entity.Property(e => e.ColSort).HasColumnType("int(11)");

                entity.Property(e => e.ColTitle).HasColumnType("varchar(200)");

                entity.Property(e => e.ColWidth).HasColumnType("int(10)");

                entity.Property(e => e.DvTitle).HasColumnType("varchar(200)");

                entity.Property(e => e.FormArea).HasColumnType("int(11)");

                entity.Property(e => e.FormHide).HasColumnType("int(11)");

                entity.Property(e => e.FormOrder).HasColumnType("int(11)");

                entity.Property(e => e.FormPlaceholder).HasColumnType("varchar(200)");

                entity.Property(e => e.FormRequired).HasColumnType("int(11)");

                entity.Property(e => e.FormSpan).HasColumnType("int(11)");

                entity.Property(e => e.FormText).HasColumnType("longtext");

                entity.Property(e => e.FormType).HasColumnType("varchar(200)");

                entity.Property(e => e.FormUrl).HasColumnType("longtext");

                entity.Property(e => e.FormValue).HasColumnType("longtext");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnType("varchar(200)");
            });
        }
    }
}

