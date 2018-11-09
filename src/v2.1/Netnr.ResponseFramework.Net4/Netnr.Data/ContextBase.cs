using Netnr.Domain;
using System.Data.Entity;

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

        public ContextBase(TypeDB typeDB = TypeDB.SQLServer) :
            base("name=SQLServerConn")
        {
            TDB = typeDB;
        }

        public virtual DbSet<SysAuthorize> SysAuthorize { get; set; }
        public virtual DbSet<SysButton> SysButton { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<TempExample> TempExample { get; set; }
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<SysTableConfig> SysTableConfig { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.SysUserId)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId1)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId2)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId4)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId3)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId5)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId6)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId7)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId8)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.OpenId9)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.Pid)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.BtnId)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.BtnClass)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.BtnIcon)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Pid)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            modelBuilder.Entity<SysRole>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.Sign)
                .IsUnicode(false);

            modelBuilder.Entity<TempExample>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<TempExample>()
                .Property(e => e.TableName)
                .IsUnicode(false);

            modelBuilder.Entity<TempExample>()
                .Property(e => e.ColField)
                .IsUnicode(false);

            modelBuilder.Entity<TempExample>()
                .Property(e => e.FormType)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.Ip)
                .IsUnicode(false);

            modelBuilder.Entity<SysTableConfig>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SysTableConfig>()
                .Property(e => e.TableName)
                .IsUnicode(false);

            modelBuilder.Entity<SysTableConfig>()
                .Property(e => e.ColField)
                .IsUnicode(false);

            modelBuilder.Entity<SysTableConfig>()
                .Property(e => e.FormType)
                .IsUnicode(false);
        }
    }
}

