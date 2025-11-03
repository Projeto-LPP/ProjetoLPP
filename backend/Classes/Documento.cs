namespace backend.Classes;
{
  public class Documento
  {
      public int DocumentoId { get; set; }
      public int ProjetoId { get; set; }
      public string UrlArquivo { get; set; } = string.Empty;
      public int EnviadoPor { get; set; }
      public DateTime DataEnvio { get; set; } = DateTime.Now;
      
      // Navigation properties
      public Projeto? Projeto { get; set; }
  }
}