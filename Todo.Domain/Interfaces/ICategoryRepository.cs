using Todo.Domain.Entities;
namespace Todo.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id);
    Task<List<Category>> ListAsync();
    Task DeleteByIdAsync(Guid id);
    Task AddAsync(Category category);
    Task<bool> ExistsWithNameAsync(string name, Guid? excludeId);
    Task UpdateAsync(Category category);
}
