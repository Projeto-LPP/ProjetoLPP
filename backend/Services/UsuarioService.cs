using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using backend.Classes;
using backend.DTOs;
using backend.interfaces;

namespace backend.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task<List<UsuarioGetResquest>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => MapToUsuarioGetRequest(u)).ToList();
        }

        public async Task<UsuarioGetResquest?> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            return await MapToUsuarioGetRequestWithStatsAsync(usuario);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return false;

            await _usuarioRepository.DeleteAsync(id);
            return true;
        }

        public async Task<UsuarioGetResquest?> UpdateUsuarioAsync(UsuarioPutResquest usuarioDto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioDto.Id);
            if (usuario == null) return null;

            // Atualizar apenas campos fornecidos
            if (!string.IsNullOrEmpty(usuarioDto.Nome))
                usuario.Nome = usuarioDto.Nome;

            if (!string.IsNullOrEmpty(usuarioDto.Email))
                usuario.Email = usuarioDto.Email;

            if (!string.IsNullOrEmpty(usuarioDto.Senha))
                usuario.Senha = _authService.HashSenha(usuarioDto.Senha);

            if (usuarioDto.Papel.HasValue)
                usuario.Papel = usuarioDto.Papel.Value;

            if (usuarioDto.BioPerfil != null)
                usuario.BioPerfil = usuarioDto.BioPerfil;

            await _usuarioRepository.UpdateAsync(usuario);
            return await MapToUsuarioGetRequestWithStatsAsync(usuario);
        }

        private UsuarioGetResquest MapToUsuarioGetRequest(Usuario usuario)
        {
            return new UsuarioGetResquest
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Papel = usuario.Papel,
                BioPerfil = usuario.BioPerfil,
                DataCadastro = usuario.DataCadastro,
                TotalProjetos = 0, // Não calcula estatísticas para lista
                ProjetosComoDono = 0,
                ProjetosComoMembro = 0
            };
        }

        private async Task<UsuarioGetResquest> MapToUsuarioGetRequestWithStatsAsync(Usuario usuario)
        {
            var totalProjetos = await _usuarioRepository.GetTotalProjetosAsync(usuario.Id);
            var projetosComoDono = await _usuarioRepository.GetProjetosComoDonoAsync(usuario.Id);
            var projetosComoMembro = await _usuarioRepository.GetProjetosComoMembroAsync(usuario.Id);

            return new UsuarioGetResquest
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Papel = usuario.Papel,
                BioPerfil = usuario.BioPerfil,
                DataCadastro = usuario.DataCadastro,
                TotalProjetos = totalProjetos,
                ProjetosComoDono = projetosComoDono,
                ProjetosComoMembro = projetosComoMembro
            };
        }
    }
}