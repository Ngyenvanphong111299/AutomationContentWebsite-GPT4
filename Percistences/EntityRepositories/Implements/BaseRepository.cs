using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Percistences.BaseResults;
using Percistences.EntityRepositories.Abstracts;
using System.Linq.Expressions;

namespace Percistences.EntityRepositories.Implements;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<EntityResult<T>> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        var saveResult = await _context.SaveChangesAsync();

        return new EntityResult<T>()
            .WithStatus(saveResult > 0)
            .WithData(entity);
    }

    public async Task<EntityResult<T>> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            var saveResult = await _context.SaveChangesAsync();

            return new EntityResult<T>()
                .WithStatus(saveResult > 0);
        }
        else
        {
            return new EntityResult<T>()
                .WithStatus(false);
        }
    }

    public async Task<EntityResult<T>> GetByConditionAsync(Expression<Func<T, bool>> condition)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(condition);
        
        return new EntityResult<T>()
            .WithStatus(entity is not null)
            .WithData(entity);
    }

    public async Task<EntityResult<T>> GetByIdAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return new EntityResult<T>()
            .WithStatus(entity is not null)
            .WithData(entity);
    }

    public IQueryable<T> StartQuery()
        => _dbSet.AsQueryable().AsNoTracking();

    public async Task<EntityResult<T>> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        var saveResult = await _context.SaveChangesAsync();

        return new EntityResult<T>()
            .WithStatus(saveResult > 0)
            .WithData(entity);
    }
}
