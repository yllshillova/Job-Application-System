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

        private readonly string entityName;
        private readonly string errorMessageNotFoundAll;
        private readonly string errorMessageNotFoundById;
        private readonly string errorMessageDto;
        private readonly string errorMessageAdd;
        private readonly string errorMessageUpdate;
        private readonly string errorMessageDelete;
        public EntityBaseRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

            entityName = typeof(TEntity).Name;
            errorMessageNotFoundAll = $"No {entityName}s could be found!";
            errorMessageNotFoundById = $"No {entityName} could be found!";
            errorMessageDto = $"Problem while mapping from/to {entityName}";
            errorMessageAdd = $"Problem adding the {entityName} entity!";
            errorMessageUpdate = $"Problem updating the {entityName} entity!";
            errorMessageDelete = $"Problem deleting the {entityName} entity!";
        }
        public async Task<Result<List<TDto>>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            if (entities == null || entities.Count == 0)
            {
                return Result<List<TDto>>.Failure(ResultErrorType.NotFound, errorMessageNotFoundAll);
            }
            var dtos = _mapper.Map<List<TDto>>(entities);
            return Result<List<TDto>>.Success(dtos);
        }

        public async Task<Result<List<TDto>>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var entities = await query.ToListAsync();
            if (entities == null || entities.Count == 0)
            {
                return Result<List<TDto>>.Failure(ResultErrorType.NotFound, errorMessageNotFoundAll);
            }
            var dtosWithParams = _mapper.Map<List<TDto>>(entities);
            return Result<List<TDto>>.Success(dtosWithParams);
        }

        public async Task<Result<TDto>> GetById(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return Result<TDto>.Failure(ResultErrorType.NotFound, errorMessageNotFoundById);
            }
            var entityDto = _mapper.Map<TDto>(entity);
            if (entityDto == null)
            {
                return Result<TDto>.Failure(ResultErrorType.BadRequest, errorMessageDto);
            }
            return Result<TDto>.Success(entityDto);
        }

        public async Task<Result<Unit>> Add(TDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            if (entity == null)
            {
                return Result<Unit>.Failure(ResultErrorType.BadRequest, errorMessageDto);
            }
            await _context.Set<TEntity>().AddAsync(entity);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<Unit>.Failure(ResultErrorType.BadRequest, errorMessageAdd);

            return Result<Unit>.Success(Unit.Value);
        }

        public async Task<Result<Unit>> Update(Guid id, TDto entityDto)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return Result<Unit>.Failure(ResultErrorType.NotFound, errorMessageNotFoundById);
            }

            var mappedEntity = _mapper.Map(entityDto, entity);
            if (mappedEntity == null)
            {
                return Result<Unit>.Failure(ResultErrorType.BadRequest, errorMessageDto);
            }
            
            EntityEntry entityEntry = _context.Entry(mappedEntity);
            entityEntry.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<Unit>.Failure(ResultErrorType.BadRequest, errorMessageUpdate);

            return Result<Unit>.Success(Unit.Value);
        }
        public async Task<Result<Unit>> Delete(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return Result<Unit>.Failure(ResultErrorType.NotFound, errorMessageNotFoundById);
            }
            EntityEntry entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Deleted;

            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return Result<Unit>.Failure(ResultErrorType.BadRequest, errorMessageDelete);

            return Result<Unit>.Success(Unit.Value);
        }
    }
}