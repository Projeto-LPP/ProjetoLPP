namespace backend.Classes
{
  public class Documento
  {
      public int Id { get; set; }
      public int ProjetoId { get; set; }
      public string UrlArquivo { get; set; } = string.Empty;
      public int EnviadoPor { get; set; }
      public DateTime DataEnvio { get; set; } = DateTime.Now;
      
      // Navegação de propriedades
      public virtual Projeto Projeto { get; set; }
      public virtual Usuario Usuario { get; set; }
  }
}