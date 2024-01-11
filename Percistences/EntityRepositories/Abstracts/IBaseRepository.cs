using Entities.Models;
using Percistences.BaseResults;
using System.Linq.Expressions;

namespace Percistences.EntityRepositories.Abstracts;

public interface IBaseRepository<T> where T : BaseEntity
{
    IQueryable<T> StartQuery();
    Task<EntityResult<T>> GetByIdAsync(Guid id);
    Task<EntityResult<T>> GetByConditionAsync(Expression<Func<T, bool>> condition);
    Task<EntityResult<T>> CreateAsync(T entity);
    Task<EntityResult<T>> UpdateAsync(T entity);
    Task<EntityResult<T>> DeleteAsync(Guid id);
}
