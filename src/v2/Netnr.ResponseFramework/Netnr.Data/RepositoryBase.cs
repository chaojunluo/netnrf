using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace Netnr.Data
{
    /// <summary>
    /// 实现泛型接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        internal ContextBase context;
        public DbSet<TEntity> dbSet;

        public RepositoryBase(ContextBase _context)
        {
            context = _context;
            dbSet = context.Set<TEntity>();
        }


        public int Insert(TEntity entity, bool isSave = true)
        {
            dbSet.Add(entity);
            return isSave ? context.SaveChanges() : 0;
        }
        public int Insert(List<TEntity> entitys, bool isSave = true)
        {
            dbSet.AddRange(entitys);
            return isSave ? context.SaveChanges() : 0;
        }


        public int Update(TEntity entity, bool isSave = true)
        {
            dbSet.Update(entity);
            return isSave ? context.SaveChanges() : 0;
        }
        public int Update(List<TEntity> entitys, bool isSave = true)
        {
            dbSet.UpdateRange(entitys);
            return isSave ? context.SaveChanges() : 0;
        }

        public int Delete(TEntity entity, bool isSave = true)
        {
            dbSet.Remove(entity);
            return isSave ? context.SaveChanges() : 0;
        }
        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            dbSet.RemoveRange(dbSet.Where(predicate));
            return isSave ? context.SaveChanges() : 0;
        }


        public TEntity FindEntity(object keyValue)
        {
            return dbSet.Find(keyValue);
        }
        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }


        public IQueryable<TEntity> IQueryable()
        {
            return dbSet;
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }


        public List<TEntity> FindList(Expression<Func<TEntity, bool>> pWhere, DataBase.Pagination pag)
        {
            var query = dbSet.AsNoTracking().Where(pWhere);

            pag.Total = query.Count();

            string OrderBys = DataBase.OrderByJoin(pag.SortName, pag.SortOrder, false);

            query = DynamicQueryableExtensions.OrderBy(query, OrderBys);

            query = query.Skip((pag.PageNumber - 1) * pag.PageSize).Take(pag.PageSize);

            return query.ToList();
        }
        public List<TEntity> FindList(IQueryable<TEntity> query, DataBase.Pagination pag)
        {
            pag.Total = query.Count();

            string OrderBys = DataBase.OrderByJoin(pag.SortName, pag.SortOrder, false);

            query = DynamicQueryableExtensions.OrderBy(query, OrderBys);

            query = query.Skip((pag.PageNumber - 1) * pag.PageSize).Take(pag.PageSize);

            return query.ToList();
        }
    }
}

