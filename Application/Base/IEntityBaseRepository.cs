using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Core;
using MediatR;

namespace Application.Base
{
    public interface IEntityBaseRepository<TEntity, TDto> where TEntity : class
    where TDto: class, new()
    {

        Task<Result<List<TDto>>> GetAll();
        Task<Result<List<TDto>>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<Result<TDto>> GetById(Guid id);
        Task<Result<Unit>> Add(TDto entity);
        Task<Result<Unit>> Update(Guid id, TDto entity);
        Task<Result<Unit>> Delete(Guid id);
    }
}