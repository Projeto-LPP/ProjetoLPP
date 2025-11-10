using System.Text.Json.Serialization;

namespace backend.Classes
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // ← Adicione esta linha
    public enum PapelUsuario
    {
        aluno = 0,
        professor = 1,
        administrador = 2
    }

    [JsonConverter(typeof(JsonStringEnumConverter))] // ← Adicione esta linha
    public enum StatusProjeto
    {
        concluido = 0,
        andamento = 1,
        disponivel = 2
    }

}