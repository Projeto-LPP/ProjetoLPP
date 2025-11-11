using backend.Classes;
using backend.DTOs;

namespace backend.interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioGetResquest>> GetAllUsuariosAsync();
        Task<UsuarioGetResquest?> GetUsuarioByIdAsync(int id);
        Task<bool> DeleteUsuarioAsync(int id);
        Task<UsuarioGetResquest?> UpdateUsuarioAsync(UsuarioPutResquest usuarioDto);
    }
}