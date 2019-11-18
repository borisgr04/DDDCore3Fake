using Domain.Base;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
       where T : BaseEntity
    {
        protected IDbContext _db;
        protected readonly DbSet<T> _dbset;


        protected GenericRepository(IDbContext context)
        {
            _db = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.AsEnumerable<T>();
        }

        public virtual T Find(object id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        protected IQueryable<T> FindByAsQueryable(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        protected IQueryable<T> AsQueryable()
        {
            return _dbset.AsQueryable();
        }

        public virtual IEnumerable<T> FindBy(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T FindFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            T query = _dbset.FirstOrDefault(predicate);
            return query;
        }
        public virtual void Add(T entity)
        {
             _dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public virtual void Edit(T entity)
        {
            _db.SetModified(entity);
        }
        public virtual void DeleteRange(List<T> entities)
        {
            _dbset.RemoveRange(entities);
        }
        public virtual void AddRange(List<T> entities)
        {
            _dbset.AddRange(entities);
        }



    }
}
