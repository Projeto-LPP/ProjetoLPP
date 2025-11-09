using Microsoft.EntityFrameworkCore;
using backend.Classes;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<ProjetoMembro> ProjetoMembros { get; set; }
        public DbSet<Documento> Documentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                
                entity.Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);
                
                entity.HasIndex(u => u.Email)
                    .IsUnique();
                
                entity.Property(u => u.Senha)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(u => u.Papel)
                    .HasConversion<string>()
                    .HasMaxLength(20);
                
                entity.Property(u => u.BioPerfil)
                    .HasMaxLength(500);
                
                entity.Property(u => u.DataCadastro)
                    .HasColumnType("timestamp with time zone");
            });

            // Configuração da entidade Projeto (se existir)
            modelBuilder.Entity<Projeto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                
                // Relação: Um usuário pode ser dono de muitos projetos
                entity.HasOne(p => p.Dono)
                      .WithMany(u => u.ProjetosComoDono)
                      .HasForeignKey(p => p.DonoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da entidade ProjetoMembro (se existir)
            modelBuilder.Entity<ProjetoMembro>(entity =>
            {
                entity.HasKey(pm => pm.Id);
                entity.Property(pm => pm.Id).ValueGeneratedOnAdd();
                
                // Relação: Um usuário pode ser membro em muitos projetos
                entity.HasOne(pm => pm.Usuario)
                      .WithMany(u => u.ProjetosComoMembro)
                      .HasForeignKey(pm => pm.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                // Relação: Um projeto pode ter muitos membros
                entity.HasOne(pm => pm.Projeto)
                      .WithMany() // Ajuste conforme sua entidade Projeto
                      .HasForeignKey(pm => pm.ProjetoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da entidade Documento (se existir)
            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).ValueGeneratedOnAdd();
                
                // Relação: Um usuário pode enviar muitos documentos
                entity.HasOne(d => d.Usuario)
                      .WithMany(u => u.DocumentosEnviados)
                      .HasForeignKey(d => d.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}