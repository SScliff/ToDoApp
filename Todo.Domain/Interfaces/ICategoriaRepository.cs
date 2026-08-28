namespace Todo.Domain.Interfaces;
using Todo.Domain.Entities;

public interface ICategoriaRepository
{
    Task<Categoria?> BuscarPorIdAsync(Guid id);
 	Task<List<Categoria>> ListarCategoriasAsync();
    Task ExcluirPorIdAsync(Guid id);
	Task<Id> CriarCategoriaAsync(string nome);
	Task ExisteIgual(string nome, Guid? ignorarId);
	Task AtualizarCategoria(Categoria categoria);
}