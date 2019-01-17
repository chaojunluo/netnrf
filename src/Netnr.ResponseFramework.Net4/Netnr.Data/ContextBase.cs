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
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysTableConfig> SysTableConfig { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<TempExample> TempExample { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.SaId)
                .IsUnicode(false);

            modelBuilder.Entity<SysAuthorize>()
                .Property(e => e.SuId)
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
                .Property(e => e.SbId)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.SbPid)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.SbBtnId)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.SbBtnClass)
                .IsUnicode(false);

            modelBuilder.Entity<SysButton>()
                .Property(e => e.SbBtnIcon)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.LogId)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.LogUrl)
                .IsUnicode(false);

            modelBuilder.Entity<SysLog>()
                .Property(e => e.LogIp)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.SmId)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.SmPid)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.SmUrl)
                .IsUnicode(false);

            modelBuilder.Entity<SysMenu>()
                .Property(e => e.SmIcon)
                .IsUnicode(false);

            modelBuilder.Entity<SysRole>()
                .Property(e => e.SrId)
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

            modelBuilder.Entity<SysUser>()
                .Property(e => e.SuId)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.SrId)
                .IsUnicode(false);

            modelBuilder.Entity<SysUser>()
                .Property(e => e.SuSign)
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
        }
    }
}