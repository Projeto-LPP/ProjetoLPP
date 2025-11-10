// Repositories/UsuarioRepository.cs
using Microsoft.EntityFrameworkCore;
using backend.Classes;
using backend.Data;

namespace backend.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.ProjetosComoDono)
                .Include(u => u.ProjetosComoMembro)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<int> GetTotalProjetosAsync(int usuarioId)
        {
            var projetosComoDono = await _context.Projetos
                .CountAsync(p => p.DonoId == usuarioId);
            
            var projetosComoMembro = await _context.ProjetoMembros
                .CountAsync(pm => pm.UsuarioId == usuarioId);
            
            return projetosComoDono + projetosComoMembro;
        }

        public async Task<int> GetProjetosComoDonoAsync(int usuarioId)
        {
            return await _context.Projetos
                .CountAsync(p => p.DonoId == usuarioId);
        }

        public async Task<int> GetProjetosComoMembroAsync(int usuarioId)
        {
            return await _context.ProjetoMembros
                .CountAsync(pm => pm.UsuarioId == usuarioId);
        }
    }
}