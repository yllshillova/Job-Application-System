using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence;

namespace Application.Base;

public class EntityBaseRepository2<TEntity> : IEntityBaseRepository2<TEntity>
    where TEntity : class, new()
{
    private readonly DataContext _context;

    public EntityBaseRepository2(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Result<List<TEntity>>> GetAll()
    {
        var entities = await _context.Set<TEntity>().ToListAsync();
        return Result<List<TEntity>>.Success(entities);
    }

    public async Task<Result<List<TEntity>>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        var entities = await query.ToListAsync();
        return Result<List<TEntity>>.Success(entities);
    }

    public async Task<Result<TEntity>> GetById(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        return Result<TEntity>.Success(entity);
    }

    public async Task<Result<Unit>> Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        var result = await _context.SaveChangesAsync() > 0;
        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to complete the creating action");
    }

    public async Task<Result<Unit>> Update(Guid id, TEntity updatedEntity)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return Result<Unit>.Failure($"Entity with ID {id} was not found!");
        }

        _context.Entry(entity).CurrentValues.SetValues(updatedEntity);

        EntityEntry entityEntry = _context.Entry(entity);
        entityEntry.State = EntityState.Modified;

        var result = await _context.SaveChangesAsync() > 0;
        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to complete the editing action!");
    }

    public async Task<Result<Unit>> Delete(Guid id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return Result<Unit>.Failure($"Entity with ID {id} was not found!");
        }

        _context.Set<TEntity>().Remove(entity);

        var result = await _context.SaveChangesAsync() > 0;
        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to complete the deleting action!");
    }
}

