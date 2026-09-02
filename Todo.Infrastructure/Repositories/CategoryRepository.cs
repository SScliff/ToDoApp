using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly TodoDbContext _context;

    public CategoryRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> ListAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return;
        }
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsWithNameAsync(string name, Guid? excludeId)
    {
        return await _context.Categories.AnyAsync(c => c.Id != excludeId && c.Name == name);
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
}
