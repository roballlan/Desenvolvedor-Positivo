using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace ProcurandoApartamento.Domain.Repositories.Interfaces
{
    public interface INoSqlFluentRepository<TEntity> where TEntity : class
    {
        INoSqlFluentRepository<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        INoSqlFluentRepository<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<IPage<TEntity>> GetPageAsync(IPageable pageable);
    }
}
