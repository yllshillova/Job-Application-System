using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Core;
using MediatR;

namespace Application.Base;

public interface IEntityBaseRepository2<TEntity> where TEntity : class, new()
{
    Task<Result<List<TEntity>>> GetAll();
    Task<Result<List<TEntity>>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<Result<TEntity>> GetById(Guid id);
    Task<Result<Unit>> Add(TEntity entity);
    Task<Result<Unit>> Update(Guid id, TEntity entity);
    Task<Result<Unit>> Delete(Guid id);
}
