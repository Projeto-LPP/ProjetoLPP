namespace backend.Classes;
{
  public enum PapelUsuario
  {
      aluno,
      mentor,
      admin
  }

  public enum StatusProjeto
  {
      concluido,
      andamento,
      disponivel
  }

  public enum PapelMembro
  {
      lider,
      colaborador,
      candidato
  }

  public enum StatusMembro
  {
      aprovado,
      pendente,
      rejeitado
  }

  public enum StatusCandidatura
  {
      pendente,
      aprovado,
      rejeitado
  }
}