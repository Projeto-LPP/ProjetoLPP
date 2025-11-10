// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.DTOs;
using backend.Services;
using backend.interfaces;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar([FromBody] UsuarioPostResquest registroDto)
        {
            try
            {
                var usuario = await _authService.RegistrarAsync(registroDto);
                
                if (usuario == null)
                {
                    return BadRequest(new { message = "Email já está em uso." });
                }

                var usuarioToken = new Classes.Usuario 
                { 
                    Id = usuario.Id, 
                    Email = usuario.Email, 
                    Nome = usuario.Nome,
                    Papel = usuario.Papel
                };

                var token = _authService.GerarTokenJWT(usuarioToken);

                return Ok(new 
                { 
                    message = "Usuário registrado com sucesso!",
                    usuario = usuario,
                    token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUsuarioDto loginDto)
        {
            try
            {
                var usuario = await _authService.LoginAsync(loginDto);
                
                if (usuario == null)
                {
                    return Unauthorized(new { message = "Email ou senha inválidos." });
                }

                var usuarioToken = new Classes.Usuario 
                { 
                    Id = usuario.Id, 
                    Email = usuario.Email, 
                    Nome = usuario.Nome,
                    Papel = usuario.Papel
                };

                var token = _authService.GerarTokenJWT(usuarioToken);

                return Ok(new 
                { 
                    message = "Login realizado com sucesso!",
                    usuario = usuario,
                    token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("perfil")]
        public async Task<ActionResult> GetPerfil()
        {
            try
            {
                var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var usuario = await _authService.GetUsuarioComEstatisticasAsync(usuarioId);
                
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
    }
}