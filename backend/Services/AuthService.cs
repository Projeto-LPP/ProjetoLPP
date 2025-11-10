// Services/AuthService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using backend.Classes;
using backend.DTOs;
using backend.interfaces;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<UsuarioGetResquest?> RegistrarAsync(UsuarioPostResquest registroDto)
        {
            if (await _usuarioRepository.EmailExistsAsync(registroDto.Email))
            {
                return null;
            }

            var usuario = new Usuario
            {
                Nome = registroDto.Nome,
                Email = registroDto.Email,
                Senha = HashSenha(registroDto.Senha),
                Papel = registroDto.Papel,
                BioPerfil = registroDto.BioPerfil,
                DataCadastro = DateTime.UtcNow
            };

            var usuarioCriado = await _usuarioRepository.CreateAsync(usuario);
            return await MapToUsuarioGetRequestAsync(usuarioCriado);
        }

        public async Task<UsuarioGetResquest?> LoginAsync(LoginUsuarioDto loginDto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
            
            if (usuario == null || !VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                return null;
            }

            return await MapToUsuarioGetRequestAsync(usuario);
        }

        public async Task<UsuarioGetResquest?> GetUsuarioComEstatisticasAsync(int usuarioId)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null) return null;

            return await MapToUsuarioGetRequestAsync(usuario);
        }

        public string GerarTokenJWT(Usuario usuario)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "backend";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "backend";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Papel.ToString()),
                new Claim("Papel", usuario.Papel.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha, string senhaHash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
        }

        private async Task<UsuarioGetResquest> MapToUsuarioGetRequestAsync(Usuario usuario)
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