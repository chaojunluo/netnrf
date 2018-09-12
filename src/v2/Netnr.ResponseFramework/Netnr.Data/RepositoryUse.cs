using System;
using System.Linq;
using Netnr.Domain;

namespace Netnr.Data
{
    /// <summary>
    /// 统一入口，保证上下文（context）的一致性
    /// 如果不想手动释放资源，请用using
    /// </summary>
    public class RepositoryUse : IDisposable
    {
        #region Save & Dispose
        public int Save()
        {
            return Context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private ContextBase _context;

        /// <summary>
        /// 上下文
        /// </summary>
        public ContextBase Context
        {
            get
            {
                return _context ?? (_context = new ContextBase());
            }
        }

        #region 单表仓库

        private RepositoryBase<SysButton> _sysbuttonRepository;
        public RepositoryBase<SysButton> SysButtonRepository
        {
            get
            {
                return _sysbuttonRepository ?? (_sysbuttonRepository = new RepositoryBase<SysButton>(Context));
            }
        }

        private RepositoryBase<SysLog> _syslogRepository;
        public RepositoryBase<SysLog> SysLogRepository
        {
            get
            {
                return _syslogRepository ?? (_syslogRepository = new RepositoryBase<SysLog>(Context));
            }
        }

        private RepositoryBase<SysMenu> _sysmenuRepository;
        public RepositoryBase<SysMenu> SysMenuRepository
        {
            get
            {
                return _sysmenuRepository ?? (_sysmenuRepository = new RepositoryBase<SysMenu>(Context));
            }
        }

        private RepositoryBase<SysRole> _sysroleRepository;
        public RepositoryBase<SysRole> SysRoleRepository
        {
            get
            {
                return _sysroleRepository ?? (_sysroleRepository = new RepositoryBase<SysRole>(Context));
            }
        }

        private RepositoryBase<SysTableConfig> _systableconfigRepository;
        public RepositoryBase<SysTableConfig> SysTableConfigRepository
        {
            get
            {
                return _systableconfigRepository ?? (_systableconfigRepository = new RepositoryBase<SysTableConfig>(Context));
            }
        }

        private RepositoryBase<SysUser> _sysuserRepository;
        public RepositoryBase<SysUser> SysUserRepository
        {
            get
            {
                return _sysuserRepository ?? (_sysuserRepository = new RepositoryBase<SysUser>(Context));
            }
        }

        private RepositoryBase<TempExample> _tempexampleRepository;
        public RepositoryBase<TempExample> TempExampleRepository
        {
            get
            {
                return _tempexampleRepository ?? (_tempexampleRepository = new RepositoryBase<TempExample>(Context));
            }
        }

        #endregion
    }
}


