using backend.Classes;

namespace backend.interfaces
{
     public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> CreateAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task<int> GetTotalProjetosAsync(int usuarioId);
        Task<int> GetProjetosComoDonoAsync(int usuarioId);
        Task<int> GetProjetosComoMembroAsync(int usuarioId);
    }
}