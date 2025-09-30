using DTOs;

namespace API.Clients
{
    public interface ICategoriaApiClient
    {
        Task<CategoriaDTO?> GetAsync(int id);
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();
        Task<CategoriaDTO> CreateAsync(CreateCategoriaRequest request);
        Task<bool> UpdateAsync(UpdateCategoriaRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExisteNombreAsync(string nombre);
    }
}