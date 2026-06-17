using System;

namespace LeveInvestimentos.Core.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string MensagemDescritiva { get; set; } = string.Empty;
        public DateTime DataLimite { get; set; }
        public string StatusTarefa { get; set; } = "Pendente";
        public int GestorId { get; set; }
        public int SubordinadoId { get; set; }
        public virtual Usuario? Subordinado { get; set; }
    }
}