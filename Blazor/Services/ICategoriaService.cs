using DTOs.Categorias;

namespace Blazor.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> GetCategoriasAsync();
        Task<CategoriaDTO> GetCategoriaByIdAsync(int id);
        Task<CategoriaDTO> CreateCategoriaAsync(CreateCategoriaRequest categoria);
        Task<CategoriaDTO> UpdateCategoriaAsync(int id, CategoriaDTO categoria);
        Task<bool> DeleteCategoriaAsync(int id);
    }
}
