using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly TodoDbContext _context;

    public CategoriaRepository(TodoDbContext context)
    {
        _context = context;
    }
    
    public async Task<Categoria?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Categorias.FirstOrDefaultAsync(c => c.IdDaCategoria == id);
    }

    public async Task<List<Categoria>> ListarCategoriasAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task ExcluirPorIdAsync(Guid id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdDaCategoria == id);
        if (categoria == null)
        {
            return;
        }
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        
    }
    public async Task<Guid> CriarCategoriaAsync(string nome,string cor){
        var categoria = new Categoria(nome, cor);
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria.IdDaCategoria;
    }

    public async Task<bool> ExisteIgualAsync(string nome, Guid? ignorarId)
    {
        return await _context.Categorias.AnyAsync(c => c.IdDaCategoria != ignorarId && c.Nome == nome);
    }

    public async Task AtualizarCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }
}