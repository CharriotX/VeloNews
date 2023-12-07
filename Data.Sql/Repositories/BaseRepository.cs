using Data.Interface.DataModels;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Sql.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected WebContext _webContext;
        protected DbSet<T> _dbSet;

        public BaseRepository(WebContext webContext)
        {
            _webContext = webContext;
            _dbSet = _webContext.Set<T>();
        }

        public T Get(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public bool Any()
            => _dbSet.Any();

        public void Save(T model)
        {
            if (model.Id > 0)
            {
                _webContext.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }

            _webContext.SaveChanges();
        }

        public void Remove(T model)
        {
            _dbSet.Remove(model);
            _webContext.SaveChanges();
        }

        public void Remove(int id)
        {
            Remove(Get(id));
        }


        public int Count()
        {
            return _dbSet.Count();
        }

        public virtual PaginatorData<T> GetPaginator(int page, int perPage, string sortField)
        {
            return GetPaginator(_dbSet, page, perPage, sortField);
        }

        public virtual PaginatorData<T> GetPaginator(IQueryable<T> initialSource, int page, int perPage, string sortField)
        {
            var dataModel = new PaginatorData<T>();

            if (sortField == null)
            {
                sortField = "Id";
            }

            var table = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(table, sortField);
            var orderBy = Expression.Lambda<Func<T, object>>
                (Expression.Convert(propertyExpression, typeof(object)), table);

            var sortedDbSet = initialSource.OrderBy(orderBy).AsQueryable<T>();

            var items = sortedDbSet
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            dataModel.Items = items;
            dataModel.TotalCount = _dbSet.Count();

            return dataModel;
        }
    }
}
