using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Base
{
    public interface IEntityBaseRepository<TEntity, TDto> where TEntity : class
    where TDto: class, new()
    {

        Task<List<TDto>> GetAll();
        Task<List<TDto>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TDto> GetById(Guid id);
        Task Add(TDto entity);
        Task Update(Guid id, TDto entity);
        Task Delete(Guid id);
    }
}