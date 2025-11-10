// Controllers/UsuariosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.DTOs;
using backend.Services;
using backend.Repositories;
using backend.interfaces;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public UsuariosController(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _authService.GetUsuarioComEstatisticasAsync(id);
                
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        [HttpPut("alterar-senha")]
        public async Task<ActionResult> AlterarSenha([FromBody] AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
                
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                if (!_authService.VerificarSenha(alterarSenhaDto.SenhaAtual, usuario.Senha))
                {
                    return BadRequest(new { message = "Senha atual incorreta." });
                }

                usuario.Senha = _authService.HashSenha(alterarSenhaDto.NovaSenha);
                await _usuarioRepository.UpdateAsync(usuario);

                return Ok(new { message = "Senha alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }
    }
}