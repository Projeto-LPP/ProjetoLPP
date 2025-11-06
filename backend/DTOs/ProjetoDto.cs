using System;
using backend.Classes;

namespace backend.DTOs
{
    // DTO para criação de projeto
    public class ProjetoPostRequest
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Area { get; set; }
        public StatusProjeto Status { get; set; } = StatusProjeto.disponivel;
        public string? Logo { get; set; }
        public int DonoId { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    }

    // DTO para atualização de projeto
    public class ProjetoPutRequest
    {   
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Area { get; set; }
        public StatusProjeto Status { get; set; } = StatusProjeto.disponivel;
        public string? Logo { get; set; }
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    }

    // DTO para checagem de projeto
    public class ProjetoGetResquest
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public StatusProjeto Status { get; set; }
        public string? Area { get; set; }
        public int DonoId { get; set; }
        public string DonoNome { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
        public string? Logo { get; set; }
        public int TotalMembros { get; set; }
        public int TotalDocumentos { get; set; }
    }

    // DTO para resumo de projeto
    public class ProjetoResumoDto
    {
        public int ProjetoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public StatusProjeto Status { get; set; }
        public string? Area { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}