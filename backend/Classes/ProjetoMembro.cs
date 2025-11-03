namespace backend.Classes;
{
  public class ProjetoMembro
  {
      public int Id { get; set; }
      public int ProjetoId { get; set; }
      public int UsuarioId { get; set; }
      
      // Navigation properties
      public Projeto? Projeto { get; set; }
      public Usuario? Usuario { get; set; }
  }
}