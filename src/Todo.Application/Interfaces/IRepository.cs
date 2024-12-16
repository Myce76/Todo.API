using Todo.Application.DTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(string entityId);
        Task<IEnumerable<T>> GetAll(GetRequest<T>? request);
        Task<T>? GetById(string entityId);
    }
}
