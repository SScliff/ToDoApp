namespace Todo.Domain.Interfaces;
using Todo.Domain.Entities;

public interface ICategoriaRepository
{
    Task<Categoria?> BuscarPorIdAsync(Guid id);
 	Task<List<Categoria>> ListarCategoriasAsync();
    Task ExcluirPorIdAsync(Guid id);
	Task<Guid> CriarCategoriaAsync(string nome, string cor);
	Task<bool> ExisteIgualAsync(string nome, Guid? ignorarId);
	Task AtualizarCategoriaAsync(Categoria categoria);
}