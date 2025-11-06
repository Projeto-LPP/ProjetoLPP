using System;
using backend.Classes;

namespace backend.DTOs
{
    // DTO para criação de usuário
    public class UsuarioPostResquest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public PapelUsuario Papel { get; set; } = PapelUsuario.aluno;
        public string? BioPerfil { get; set; }
    }

    // DTO para atualização de usuário
    public class UsuarioPutResquest
    {   
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public PapelUsuario? Papel { get; set; }
        public string? BioPerfil { get; set; }
    }

    // DTO para resposta (retorno)
    public class UsuarioGetResquest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public PapelUsuario Papel { get; set; }
        public string? BioPerfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public int TotalProjetos { get; set; }
        public int ProjetosComoDono { get; set; }
        public int ProjetosComoMembro { get; set; }
    }

    // DTO para login
    public class LoginUsuarioDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    // DTO para alteração de senha
    public class AlterarSenhaDto
    {
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}