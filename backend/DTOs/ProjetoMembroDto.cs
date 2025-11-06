using System;
using backend.Classes;

namespace backend.DTOs
{
    // Resumo de usuário para incluir em respostas de membro
    public class UsuarioResumoDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public PapelUsuario Papel { get; set; }
        public string? BioPerfil { get; set; }
    }

    // DTO para adicionar membro a um projeto
    public class ProjetoMembroPostRequest
    {
        public int UsuarioId { get; set; }
        public int ProjetoId { get; set; }
    }

    // DTO para resposta de membro
    public class ProjetoMembroPutRequest
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }
    }

    // DTO para listagem de membros
    public class ProjetoMembroGetRequest
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }

        // Informação do usuário embarcada
        public UsuarioResumoDto? Usuario { get; set; }
    }
}