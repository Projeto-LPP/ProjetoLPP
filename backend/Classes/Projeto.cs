using System.Text.Json.Serialization;
namespace backend.Classes
{
  public class Projeto
  {
      public int ProjetoId { get; set; }
      public string Titulo { get; set; } = string.Empty;
      public string? Descricao { get; set; }
      public StatusProjeto Status { get; set; } = StatusProjeto.disponivel;
      public string? Area { get; set; }
      public int DonoId { get; set; }
      public DateTime CriadoEm { get; set; } = DateTime.Now;
      public DateTime AtualizadoEm { get; set; } = DateTime.Now;
      public string? Logo { get; set; }
      
      // Navigation properties
      public Usuario? Dono { get; set; }
      public List<ProjetoMembro>? Membros { get; set; }
      public List<Documento>? Documentos { get; set; }
  }
}