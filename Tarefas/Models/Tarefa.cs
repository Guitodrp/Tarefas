using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Tarefas.Models;

public class Tarefa(string titulo, string descricao)
{
    public enum EStatus : short {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Finalizada")]
        Finalizada = 2,
        [Description("Cancelada")]
        Cancelada = 3
    }
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = titulo;
    public string Descricao { get; set; } = descricao;
    public DateTime Data { get; set; } = DateTime.Now;
    public EStatus Status { get; set; } = EStatus.Pendente;

}
