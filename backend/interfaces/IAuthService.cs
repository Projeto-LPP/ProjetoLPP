using backend.Classes;
using backend.DTOs;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<UsuarioGetResquest?> RegistrarAsync(UsuarioPostResquest registroDto);
        Task<UsuarioGetResquest?> LoginAsync(LoginUsuarioDto loginDto);
        string GerarTokenJWT(Usuario usuario);
        bool VerificarSenha(string senha, string senhaHash);
        string HashSenha(string senha);
        Task<UsuarioGetResquest?> GetUsuarioComEstatisticasAsync(int usuarioId);
    }
}