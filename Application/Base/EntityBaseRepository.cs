using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence;

namespace Application.Base
{
    public class EntityBaseRepository<TEntity, TDto> : IEntityBaseRepository<TEntity, TDto> where TEntity : class 
    where TDto : class, new()
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EntityBaseRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<List<TDto>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            var dtos = _mapper.Map<List<TDto>>(entities);
            return dtos;
        }

        public async Task<List<TDto>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var entities = await query.ToListAsync();
            var dtosWithParams = _mapper.Map<List<TDto>>(entities);
            return dtosWithParams;
        }

        public async Task<TDto> GetById(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            var entityDto = _mapper.Map<TDto>(entity);
            return entityDto;
        }

        public async Task Add(TDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, TDto entityDto)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return;

            _mapper.Map(entityDto, entity);

            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        public async Task Delete(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return;
            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}