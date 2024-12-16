using Microsoft.EntityFrameworkCore;

using Todo.Application.DTOs;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Infrastructure.Persistence;

namespace Todo.Business.Repositories;

public class CosmoRepository<T> : IRepository<T> where T : BaseEntity
{
    //The context is added in Step 5.1
    private readonly TodoContext _context;

    public CosmoRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        var addedEntity = (await _context.AddAsync(entity)).Entity;
        _context.SaveChanges();
        return addedEntity;
    }

    public void Delete(string entityId)
    {
        var entity = _context.Find<T>(entityId);
        if (entity != null) _context.Remove(entity);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<T>> GetAll(GetRequest<T>? request)
    {
        IQueryable<T> query = _context.Set<T>();

        if (request != null)
        {
            if (request.Filter != null)
            {
                query = query.Where(request.Filter);
            }

            if (request.OrderBy != null)
            {
                query = request.OrderBy(query);
            }

            if (request.Skip.HasValue)
            {
                query = query.Skip(request.Skip.Value);
            }

            if (request.Take.HasValue)
            {
                query = query.Take(request.Take.Value);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<T>? GetById(string entityId)
    {
        return await _context.FindAsync<T>(entityId);
    }

    public async Task<T> Update(T entity)
    {
        var updatedEntity = _context.Update(entity).Entity;
        await _context.SaveChangesAsync();
        return updatedEntity;
    }
}