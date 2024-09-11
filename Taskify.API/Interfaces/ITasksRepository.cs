using Taskify.API.Models;

namespace Taskify.API.Interfaces
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllAsync();
        Task<IEnumerable<Tasks>> FilterByDateAsync(DateTime date);
        Task<IEnumerable<Tasks>> FilterByPriorityAsync(string priority);
        Task<Tasks?> GetByIdAsync(int id);
        Task<Tasks> AddAsync (Tasks task);
        Task<Tasks> UpdateAsync (Tasks task);
        Task<Tasks> RemoveAsync (Tasks task);
    }
}
