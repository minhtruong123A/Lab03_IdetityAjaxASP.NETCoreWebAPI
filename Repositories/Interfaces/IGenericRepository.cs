﻿using System.Linq.Expressions;

namespace Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdIncludeAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindOneAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        IQueryable<T> GetAllAsQueryable();
        Task<List<TResult>> GetAllWithParamAsync<TResult>(string queryParam,
            Expression<Func<T, bool>> filter = null, Expression<Func<T, TResult>> select = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null);
    }
}
