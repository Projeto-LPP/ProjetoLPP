namespace backend.Classes
{
  public class Usuario
  {
      public int Id { get; set; }
      public string Nome { get; set; } = string.Empty;
      public string Email { get; set; } = string.Empty;
      public string Senha { get; set; } = string.Empty;
      public PapelUsuario Papel { get; set; } = PapelUsuario.aluno;
      public string? BioPerfil { get; set; }
      public DateTime DataCadastro { get; set; } = DateTime.Now;

      // Navegação de propriedades
      // Inicializamos coleções para evitar avisos de propriedades não inicializadas (CS8618)
      public virtual ICollection<Projeto> ProjetosComoDono { get; set; } = new List<Projeto>();
      public virtual ICollection<ProjetoMembro> ProjetosComoMembro { get; set; } = new List<ProjetoMembro>();
      public virtual ICollection<Documento> DocumentosEnviados { get; set; } = new List<Documento>();
  }
}