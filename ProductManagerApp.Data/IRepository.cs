using System;
using System.Linq;
using System.Linq.Expressions;
using ProductManagerApp.Domain;

namespace ProductManagerApp.Data
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Items { get; }
        IQueryable<T> ItemsIncluding(params Expression<Func<T, object>>[] paths);
        void Add(T item);
        void Delete(T item);
        void Delete(int id);
        T FindById(int id);
    }
}
