using DTOs;

namespace Application.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaDTO?> GetAsync(int id);
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();
        Task<CategoriaDTO> CreateAsync(CreateCategoriaRequest request);
        Task<bool> UpdateAsync(UpdateCategoriaRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExisteNombreAsync(string nombre);
    }
}