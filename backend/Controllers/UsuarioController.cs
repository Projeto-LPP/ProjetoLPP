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
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios - Listar todos os usuários (apenas admin)
        [HttpGet]
        [Authorize(Roles = "administrador")]
        public async Task<ActionResult> GetAllUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        // GET: api/usuarios/5 - Obter usuário por ID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                
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

        // PUT: api/usuarios/5 - Atualizar usuário
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuario(int id, [FromBody] UsuarioPutResquest usuarioDto)
        {
            try
            {
                if (id != usuarioDto.Id)
                {
                    return BadRequest(new { message = "ID do usuário não corresponde." });
                }

                var usuarioAtualizado = await _usuarioService.UpdateUsuarioAsync(usuarioDto);
                
                if (usuarioAtualizado == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(new { 
                    message = "Usuário atualizado com sucesso!", 
                    usuario = usuarioAtualizado 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        // DELETE: api/usuarios/5 - Excluir usuário (apenas admin ou próprio usuário)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            try
            {
                var success = await _usuarioService.DeleteUsuarioAsync(id);
                
                if (!success)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(new { message = "Usuário excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", error = ex.Message });
            }
        }

        // GET: api/usuarios/me - Obter dados do usuário logado
        [HttpGet("me")]
        public async Task<ActionResult> GetMe()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
                var usuario = await _usuarioService.GetUsuarioByIdAsync(userId);
                
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