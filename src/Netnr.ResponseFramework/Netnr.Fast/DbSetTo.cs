#if NET40
using System.Collections.Generic;
using System.Data.Entity;

public static class DbSetTo
{
    public static void Update<TEntity>(this DbSet<TEntity> entities, TEntity entity, DbContext context) where TEntity : class
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public static void UpdateRange<TEntity>(this DbSet<TEntity> entities, List<TEntity> entitys, DbContext context) where TEntity : class
    {
        foreach (var entity in entitys)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
#endif