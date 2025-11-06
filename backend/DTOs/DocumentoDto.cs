using System;

namespace backend.DTOs
{
    // DTO usado para criar um Documento
    public class DocumentoPostRequest
    {
        public int ProjetoId { get; set; }
        public string UrlArquivo { get; set; } = string.Empty;
        public int EnviadoPor { get; set; }
        public DateTime DataEnvio { get; set; } = DateTime.UtcNow;
    }

    // DTO usado para atualizar um Documento existente
    public class DocumentoPutRequest
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string UrlArquivo { get; set; } = string.Empty;
        public int EnviadoPor { get; set; }
        public DateTime DataEnvio { get; set; }
    }

    // DTO usado para retornar dados de Documento via API
    public class DocumentoGetResponse
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string UrlArquivo { get; set; } = string.Empty;
        public int EnviadoPor { get; set; }
        public DateTime DataEnvio { get; set; }
        
        // Campos opcionais para enriquecer a resposta (nome do usuário, título do projeto)
        public string? UsuarioNome { get; set; }
        public string? ProjetoTitulo { get; set; }
    }
}
