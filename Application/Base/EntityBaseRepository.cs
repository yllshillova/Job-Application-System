using System.Linq.Expressions;
using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence;

namespace Application.Base
{
    public class EntityBaseRepository<TEntity, TDto> : IEntityBaseRepository<TEntity, TDto>
    where TEntity : class
    where TDto : class, new()
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EntityBaseRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<Result<List<TDto>>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            var dtos = _mapper.Map<List<TDto>>(entities);
            return Result<List<TDto>>.Success(dtos);
        }

        public async Task<Result<List<TDto>>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var entities = await query.ToListAsync();
            var dtosWithParams = _mapper.Map<List<TDto>>(entities);
            return Result<List<TDto>>.Success(dtosWithParams);
        }

        public async Task<Result<TDto>> GetById(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            var entityDto = _mapper.Map<TDto>(entity);
            return Result<TDto>.Success(entityDto);
        }

        public async Task<Result<Unit>> Add(TDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            await _context.Set<TEntity>().AddAsync(entity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<Unit>.Failure(ResultErrorType.BadRequest);

            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<Unit>> Update(Guid id, TDto entityDto)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return Result<Unit>.Failure(ResultErrorType.NotFound);
            }

            _mapper.Map(entityDto, entity);

            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<Unit>.Failure(ResultErrorType.BadRequest);

            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<Unit>> Delete(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return Result<Unit>.Failure(ResultErrorType.NotFound);
            }
            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Deleted;

            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<Unit>.Failure(ResultErrorType.BadRequest);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}