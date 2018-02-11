using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ProductManagerApp.Domain;
using ProductManagerApp.Services;

namespace ProductManagerApp.Data
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        private DbContext DbContext { get; }
        private DbSet<T> DbSet { get; }
        public EntityFrameworkRepository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }
        public IQueryable<T> Items => DbSet;

        public IQueryable<T> ItemsIncluding(params Expression<Func<T, object>>[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            return paths.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
                (current, includeProperty) => current.Include(includeProperty));
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Id != 0)
            {
                throw new InvalidOperationException("Cannot add entity with already set Id");
            }

            DbSet.Add(item);
        }

        public void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            DbSet.Remove(item);
        }

        public void Delete(int id)
        {
            var item = FindById(id);
            if (item != null)
            {
                Delete(item);
            }
        }

        public T FindById(int id)
        {
            return DbSet.Find(id);
        }
    }
}
